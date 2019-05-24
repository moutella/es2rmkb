using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorJogo : MonoBehaviour
{
    public const int JOGADOR=0;
    public const int CPU=1;
    float cronometroAtual;
    public Color32[] coresDoJogo;
    public maoUI maoInterface;
    private Tabuleiro tabuleiroAtual;
    private Deck deckAtual;
    private ArrayList tabuleirosValidos;
    private int turno; //0 é turno do jogador, 1 da ia
    //Isso pode ser feito dentro da classe do jogador futuramente

    void Start()
    {
        tabuleirosValidos = new ArrayList();
        StartCoroutine(GameStart());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("---------------------------------------------------VERIFICANDO CONJUNTOS DA MESAATUAL-------------------------");
            int contador = 1;
            foreach(Conjunto c in tabuleiroAtual.getConjuntos()){
                Debug.Log("------------------- Conjunto: " + contador++ + "--Valido: " + c.getValida() + "----------------");
                c.printaPecas();
            }

        }
    }
    public Tabuleiro getTabuleiroAtual()
    {
        return tabuleiroAtual;
    }
    private IEnumerator GameStart()
    {
        deckAtual = new Deck();
        tabuleiroAtual = new Tabuleiro();
        Tabuleiro cloneBase = tabuleiroAtual.cloneTabuleiro();
        maoInterface.setMaoInicial(deckAtual.pegaCartasIniciais());
        tabuleirosValidos.Add(cloneBase);
        yield return null;
    }

    public void setTurno(int vez){
        /*Fazendo uma função separada para setar o turno para o caso da implementação ser mudada
        Assim, nós não precisaremos alterar em todos os locais onde o turno for setado*/
        this.turno = vez;
    }

    public void sorteiaPrimeiroJogador(){
        /*Neste método vale colocar alguma animação entre os comandos de pegar peça*/
        Deck deckAux = new Deck();
        deckAux.removeCoringas();
        Peca pecaPlayer = deckAux.pegaPecaAleatoria();
        Peca pecaCPU = deckAux.pegaPecaAleatoria();
        while(pecaPlayer.getValor()==pecaCPU.getValor() && pecaPlayer.getCodigoCor()==pecaCPU.getCodigoCor()){
            pecaPlayer = deckAux.pegaPecaAleatoria();
            pecaCPU = deckAux.pegaPecaAleatoria();
        }
        if(pecaPlayer.getValor()>pecaCPU.getValor()) setTurno(JOGADOR);
        else if(pecaPlayer.getValor()<pecaCPU.getValor()) setTurno(CPU);
        else{
            if(pecaPlayer.getCodigoCor()>pecaCPU.getCodigoCor())setTurno(JOGADOR);
            else setTurno(CPU);
        }
        
    }

    
    public IEnumerator IniciaContagem(float tempoMax = 60)
    {
        cronometroAtual = tempoMax;
        while (cronometroAtual > 0)
        {
            //Mostrar ao usuário no jogo
            Debug.Log("Tempo: " + cronometroAtual);
            yield return new WaitForSeconds(1.0f);
            cronometroAtual--;
        }

        terminaJogada();
    }

    public void iniciaTurno(){
        Tabuleiro cloneBase = tabuleiroAtual.cloneTabuleiro();
        tabuleirosValidos.Add(cloneBase);
        maoInterface.fazBackup();

        StartCoroutine(IniciaContagem());
    }

    public void terminaJogada()
    {
        if(tabuleiroAtual.validaTabuleiro() && maoInterface.jogouAlgumaPeca()){
            if(maoInterface.getPrimeiraJogada()){
                int pontos = maoInterface.getPontosDaJogada();
                if(pontos>=30){
                    maoInterface.setPrimeiraJogada(false);
                    maoInterface.limpaJogada();
                    this.setTurno(CPU);
                }else{
                    avisoJogadaInvalida();
                    rollbackJogada();//Faz sentido chamar esse método só se estoura o tempo e não em todo fim de jogada
                    penalizacaoTimeout();
                }
            }else{
                this.setTurno(CPU); //Tem que ter checagem do turno antes das ações, na parte gráfica
            }
        }else{
            avisoJogadaInvalida();
            rollbackJogada();//Faz sentido chamar esse método só se estoura o tempo e não em todo fim de jogada
            penalizacaoTimeout();
        }   
    }

    public void rollbackJogada(){
        maoInterface.rollbackPecas();
        rollbackConjuntos();
    }

    public void rollbackConjuntos() {
        Tabuleiro tabuleiroBackup = (Tabuleiro)tabuleirosValidos[tabuleirosValidos.Count-1];
        tabuleiroAtual = tabuleiroBackup.cloneTabuleiro();
    }

    public void avisoJogadaInvalida(){
        //TODO: Ter algum retorno ao usuário de que a jogada dele não foi válida
    }

    public void penalizacaoTimeout(){
    	//TODO: Realizar as penalizações caso o usuário estoure o tempo
    }
}
