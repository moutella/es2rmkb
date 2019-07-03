using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorJogo : MonoBehaviour
{
    public const float OFFSET=34.7f;
    public const int JOGADOR=0;
    public const int CPU=1;
    float cronometroAtual;
    public Color32[] coresDoJogo;
    public maoUI maoInterface;
    public IA maoIA;
    private Tabuleiro tabuleiroAtual;
    public TabuleiroInterface controlaTabInterface;
    private Deck deckAtual;
    private ArrayList tabuleirosValidos;
    private int turno; //0 é turno do jogador, 1 da ia
    public bool modoConjunto = false;
    public bool isBotandoPeca;
    public bool paraCronometro;
    public ConjuntoInterfaceCreator criadorDeConjuntos;
    public GameObject cara;

    public Text contadorTexto;
    //Isso pode ser feito dentro da classe do jogador futuramente

    void Start()
    {
        cara = GameObject.FindGameObjectWithTag("Cara");
        maoIA = new IA();
        contadorTexto = GameObject.Find("contador").GetComponent<Text>();
        tabuleirosValidos = new ArrayList();
        paraCronometro=false;
        StartCoroutine(GameStart());
    }

    // Update is called once per frame
    void Update()
    {
        if(maoInterface.getComprouPeca()){
            //muda o turno
            flipaTurno();
            maoInterface.setComprouPeca(false);
            StartCoroutine(turnoIA());
        }

        //------------------------------------------COISAS PARA USAR COMO DEBUG---------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Para ver estado dos gameobjects quando pressionados
            Debug.Break();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("---------------------------------------------------VERIFICANDO CONJUNTOS DA MESAATUAL-------------------------");
            int contador = 1;
            foreach(Conjunto c in tabuleiroAtual.getConjuntos()){
                Debug.Log("------------------- Conjunto: " + contador++ + "--Valido: " + c.getValida() + "----------------");
                Debug.Log("Conjunto pos:" + c.getPos()); 
                c.printaPecas();
            }
            Debug.Log("Contagem de conjuntos no backup: " + ((Tabuleiro)tabuleirosValidos[tabuleirosValidos.Count - 1]).getConjuntos().Count);

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
            }else if(getTurno(CPU)){
                StartCoroutine(turnoIA());
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            print("Rollback");
            rollbackJogada();
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
        if(this.turno==1){
            Vector3 v = cara.transform.position;
            cara.transform.position= new Vector3(v.x, v.y-OFFSET, v.z); 
        }
        deckAtual = new Deck();
        tabuleiroAtual = new Tabuleiro();
        maoInterface.setMaoInicial(deckAtual.pegaCartasIniciais());
        maoIA.insereMaoInicial(deckAtual.pegaCartasIniciais());
        if(getTurno(JOGADOR)){
            this.iniciaTurno();
        }else{
            StartCoroutine(turnoIA());
        }
        tabuleirosValidos.Add(tabuleiroAtual.cloneTabuleiro());
        yield return null;
    }

    public void compraCarta()
    {
        if(getTurno(JOGADOR)){
            if (maoInterface.maoLogica.getPecas().Count < 24) { 
                Peca p = deckAtual.pegaPecaAleatoria();
                maoInterface.compraPeca(p);
            }
        }else{
            maoIA.compraPeca(deckAtual);
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
        //Debug.Log("Tempo: " + cronometroAtual);
        while (cronometroAtual > 0 && !paraCronometro)
        {
            //Mostrar ao usuário no jogo
            Debug.Log("Tempo: " + cronometroAtual);
            contadorTexto.text = cronometroAtual.ToString();
            yield return new WaitForSeconds(1.0f);
            cronometroAtual--;
        }


        if(cronometroAtual<=0)terminaJogada();

        paraCronometro=false;
    }

    public void iniciaTurno(){

        cronometroAtual=60;
        Tabuleiro cloneBase = tabuleiroAtual.cloneTabuleiro();
        tabuleirosValidos.Clear();
        tabuleirosValidos.Add(cloneBase);
        maoInterface.fazBackup();

        StartCoroutine(IniciaContagem());
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
                        bool fim = checaFim();
                        if(fim){
                            vaiParaTelaDeFim();
                        }else{
                            flipaTurno();
                        }
                        return true;
                    }else{
                        avisoJogadaInvalida();
                        if(cronometroAtual<=0){
                            rollbackJogada();//Fazer verificação de tempo para saber se utiliza rollback
                            penalizacaoTimeout();
                        }
                        
                        return false;
                    }
                }else{
                    maoInterface.limpaJogada();
                    bool fim = checaFim();
                    if(fim){
                        vaiParaTelaDeFim();
                    }else{
                        flipaTurno();
                    }
                    
                    return true;
                }
            }else{
                avisoJogadaInvalida();
                if(cronometroAtual<=0){
                    rollbackJogada();//Fazer verificação de tempo para saber se utiliza rollback
                    penalizacaoTimeout();
                }
                return false;
            }
        }else{
            //A IA vai calcular as jogadas possíveis, apenas... Nunca vai fazer uma jogada que permita um tabuleiro inválido
            //Dessa forma, acho que não é necessária validação aqui...
            flipaTurno();
            iniciaTurno();//inicia turno do jogador
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
        controlaTabInterface.reset();
        foreach (Conjunto c in tabuleiroAtual.getConjuntos())
        {
            criadorDeConjuntos.inicializaParaRollback(c);
        }
        
    }

    public void avisoJogadaInvalida(){
        //TODO: Ter algum retorno ao usuário de que a jogada dele não foi válida
        Debug.Log("JOGADA INVÁLIDA");
    }

    public void penalizacaoTimeout(){
        for(int i=0;i<3;i++){
            this.compraCarta();
        }
    }
    public void flipaModo()
    {
        modoConjunto = !modoConjunto;
    }
    public void flipaTurno(){
        Vector3 v = cara.transform.position;
        if(this.turno==CPU){
            setTurno(JOGADOR);
            cara.transform.position= new Vector3(v.x, v.y+OFFSET, v.z);
        }
        else {
            setTurno(CPU);
            paraCronometro=true;
            cara.transform.position= new Vector3(v.x, v.y-OFFSET, v.z);
        }


    }

    public void iaJogaNoTabuleiro(){
        //DÁ PARA SETAR DIFICULDADE DA IA, ESCOLHENDO ALEATORIA OU A QUE JOGA O MAIOR NUMERO DE PECAS
        Jogada escolhida = maoIA.retornaJogadaAleatoria();
        if(escolhida==null){
            Debug.Log("----------------------------------COMPROU-------------------------------------\n");
            compraCarta();
        }else{
            Debug.Log("----------------------------------Jogada IA-------------------------------------\n");
            foreach (SubJogada sj in escolhida.subjogadas)
            {
                Debug.Log("**Subjogada**");
                Debug.Log("Tipo: " + sj.tipo);
                Debug.Log("Conjunto: " + tabuleiroAtual.getConjuntos().LastIndexOf(sj.pai));
                sj.pai.printaPecas();

            }
            //Chama alguma função de interface que joga na tela as peças inseridas



            if(maoIA.getPrimeiraJogada())maoIA.setPrimeiraJogada(false);
        } 
    }

    IEnumerator turnoIA(){
        yield return new WaitForSecondsRealtime(5.0f);
        iaJogaNoTabuleiro();
        terminaJogada();
    }

    public maoUI getJogador(){
        return this.maoInterface;
    }

    public bool checaFim(){
        return (maoInterface.ehVazia() || maoIA.ehVazia());
    }

    public void vaiParaTelaDeFim(){
        if(this.turno==0){
            //Vai para tela você ganhou
        }else{
            //Vai para tela você perdeu
        }
    }
}
