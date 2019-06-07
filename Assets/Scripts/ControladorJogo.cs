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
    public TabuleiroInterface controlaTabInterface;
    private Deck deckAtual;
    private ArrayList tabuleirosValidos;
    private int turno; //0 é turno do jogador, 1 da ia
    public bool modoConjunto = false;
    public bool isBotandoPeca;
    //Isso pode ser feito dentro da classe do jogador futuramente

    void Start()
    {
        tabuleirosValidos = new ArrayList();
        StartCoroutine(GameStart());
    }

    // Update is called once per frame
    void Update()
    {
        if(maoInterface.getComprouPeca()){
            //muda o turno
            flipaTurno();
            maoInterface.setComprouPeca(false);
        }

        //------------------------------------------COISAS PARA USAR COMO DEBUG---------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("---------------------------------------------------VERIFICANDO CONJUNTOS DA MESAATUAL-------------------------");
            int contador = 1;
            foreach(Conjunto c in tabuleiroAtual.getConjuntos()){
                Debug.Log("------------------- Conjunto: " + contador++ + "--Valido: " + c.getValida() + "----------------");
                c.printaPecas();
            }

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            maoInterface.reset();
            controlaTabInterface.reset();
            StartCoroutine(GameStart());
            
        }

        if(Input.GetKeyDown(KeyCode.F)){
            bool terminada = terminaJogada();
            if(getTurno(JOGADOR) && terminada){
                iniciaTurno();
            }
        }
        //------------------------------------------COISAS PARA USAR COMO DEBUG---------------------------------------------------------------
        
    }
    public Tabuleiro getTabuleiroAtual()
    {
        return tabuleiroAtual;
    }
    private IEnumerator GameStart()
    {
        sorteiaPrimeiroJogador();
        Debug.Log("Quem começa: " + this.turno);
        deckAtual = new Deck();
        tabuleiroAtual = new Tabuleiro();
        maoInterface.setMaoInicial(deckAtual.pegaCartasIniciais());
        if(getTurno(JOGADOR)){
            this.iniciaTurno();
        }
        
        yield return null;
    }
    public void compraCarta()
    {
        if(getTurno(JOGADOR)){
            if (maoInterface.maoLogica.getPecas().Count < 24) { 
                Peca p = deckAtual.pegaPecaAleatoria();
                maoInterface.compraPeca(p);
            }
        }
    }

    public bool getTurno(int player){
        if(this.turno == player)return true;
        return false;
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
        tabuleirosValidos.Clear();
        tabuleirosValidos.Add(cloneBase);
        maoInterface.fazBackup();

        //StartCoroutine(IniciaContagem());
    }

    public bool terminaJogada()
    {
        if(getTurno(JOGADOR)){
            Debug.Log("Tabuleiro válido?" + tabuleiroAtual.validaTabuleiro());
            Debug.Log("Jogou Peça?" + maoInterface.jogouAlgumaPeca());
            if(tabuleiroAtual.validaTabuleiro() && maoInterface.jogouAlgumaPeca()){
                if(maoInterface.getPrimeiraJogada()){
                    Debug.Log("Primeira jogada:");
                    int pontos = maoInterface.getPontosDaJogada();
                    Debug.Log(pontos + " Pontos");
                    if(pontos>=30){
                        Debug.Log("passou");
                        maoInterface.setPrimeiraJogada(false);
                        maoInterface.limpaJogada();
                        flipaTurno();
                        return true;
                    }else{
                        avisoJogadaInvalida();
                        //rollbackJogada();//Fazer verificação de tempo para saber se utiliza rollback
                        //penalizacaoTimeout();
                        return false;
                    }
                }else{
                    maoInterface.limpaJogada();
                    flipaTurno();
                    return true;
                }
            }else{
                avisoJogadaInvalida();
                //rollbackJogada();//Fazer verificação de tempo para saber se utiliza rollback
                //penalizacaoTimeout();
                return false;
            }
        }else{
            //A IA vai calcular as jogadas possíveis, apenas... Nunca vai fazer uma jogada que permita um tabuleiro inválido
            //Dessa forma, acho que não é necessária validação aqui...
            flipaTurno();
            return true;
            //Por enquanto só flipa turno, até termos ia implementada
            
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
        Debug.Log("JOGADA INVÁLIDA");
    }

    public void penalizacaoTimeout(){
    	//TODO: Realizar as penalizações caso o usuário estoure o tempo
    }
    public void flipaModo()
    {
        modoConjunto = !modoConjunto;
    }
    public void flipaTurno(){
        if(this.turno==CPU)setTurno(JOGADOR);
        else setTurno(CPU);
    }
}
