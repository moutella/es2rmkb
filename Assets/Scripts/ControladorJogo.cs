using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorJogo : MonoBehaviour
{
    private const int JOGADOR=0;
    private const int CPU=1;
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
    void FixedUpdate()
    {
        
    }
    private IEnumerator GameStart()
    {
        deckAtual = new Deck();
        tabuleiroAtual = new Tabuleiro();
        Tabuleiro cloneBase = tabuleiroAtual.cloneTabuleiro();
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
}
