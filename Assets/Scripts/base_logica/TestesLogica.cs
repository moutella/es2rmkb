using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestesLogica : MonoBehaviour
{
    Tabuleiro tabuleiro;
    //CLASSE PURAMENTE PARA TESTES, ADICIONE O PREFAB TestesLogica a cena para ver o output no console
    void Start() //trate como o main
    {
        Deck deck = new Deck();
        MaoUsuario MaoUsuario = new MaoUsuario();
        IA maoIA = new IA();
        //MaoUsuario.insereMaoInicial(deck.pegaCartasIniciais());

        Peca p = new Peca(1,1,false);
        maoIA.inserePeca(p);
        p = new Peca(0,5,false);
        maoIA.inserePeca(p);
        p = new Peca(1,5,false);
        maoIA.inserePeca(p);
        p = new Peca(2,5,false);
        maoIA.inserePeca(p);
        p = new Peca(3,5,false);
        maoIA.inserePeca(p);
        p = new Peca(0,7,false);
        maoIA.inserePeca(p);
        p = new Peca(1,7,false);
        maoIA.inserePeca(p);
        p = new Peca(2,7,false);
        maoIA.inserePeca(p);
        p = new Peca(0,8,false);
        maoIA.inserePeca(p);
        p = new Peca(1,8,false);
        maoIA.inserePeca(p);
        p = new Peca(2,8,false);
        maoIA.inserePeca(p);
        p = new Peca(0,10,false);
        maoIA.inserePeca(p);
        p = new Peca(0,12,false);
        maoIA.inserePeca(p);
        p = new Peca(1,11,false);
        maoIA.inserePeca(p);

        ArrayList grupos = maoIA.retornaTodosOsConjuntosDaMao();
        Debug.Log("VAI MOSTRAR A LISTA DE TODAS AS JOGADAS POSSIVEIS\n\n");
        foreach (ArrayList al in grupos)
        {
            Debug.Log("----------------------------------Jogada Possível-------------------------------------\n");
            foreach (Conjunto c in al)
            {
                Debug.Log("**Inicio de Conjunto**");
                c.printaPecas();
            }
            
        }
        
        //MaoUsuario.printaPecas();
        //tabuleiro = new Tabuleiro();
        

        /*
        Conjunto seq = new Conjunto();
        
        Peca seq1 = new Peca(0, 1, false);
        
        Peca seq2 = new Peca(0, 2, false);
        
        Peca seq3 = new Peca(0, 1, true);
        seq.inserePeca(seq1);
        seq.inserePeca(seq2);
        seq.inserePeca(seq3);

        tabuleiro.insereConjunto(seq);
        Debug.Log(seq.validaConjunto());

        //Conjunto c = new Conjunto(); 
        //Peca p = new Peca(0, 1, false);
        //Peca p1 = new Peca(1, 1, false);
        //Peca p2 = new Peca(2, 1, false);
        //Peca p3 = new Peca(2, 1, true);
        //c.inserePeca(p);
        //c.inserePeca(p1);
        //c.inserePeca(p2);
        //c.inserePeca(p3);
        //tabuleiro.insereConjunto(c);

        //Debug.Log(c.validaConjunto());

        */
        //Debug.Log(tabuleiro.validaTabuleiro());
    }
    
}
