using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestesLogica : MonoBehaviour
{
    Tabuleiro tabuleiro;
    //CLASSE PURAMENTE PARA TESTES, ADICIONE O PREFAB TestesLogica a cena para ver o output no console
    void Start() //trate como o main
    {
        tabuleiro = new Tabuleiro();
        
        Conjunto seq = new Conjunto();
        Peca seq2 = new Peca(0, 1, false);
        Peca seq3 = new Peca(0, 2, false);
        Peca seq1 = new Peca(0, 1, true);
        seq.inserePeca(seq1);
        seq.inserePeca(seq2);
        seq.inserePeca(seq3);
        tabuleiro.insereConjunto(seq);
        Debug.Log(seq.validaConjunto());

        Conjunto c = new Conjunto(); 
        Peca p = new Peca(0, 1, false);
        Peca p1 = new Peca(1, 1, false);
        Peca p2 = new Peca(2, 1, false);
        Peca p3 = new Peca(2, 1, true);
        c.inserePeca(p);
        c.inserePeca(p1);
        c.inserePeca(p2);
        c.inserePeca(p3);
        tabuleiro.insereConjunto(c);

        Debug.Log(c.validaConjunto());
        Debug.Log(tabuleiro.validaTabuleiro());
    }
    
}
