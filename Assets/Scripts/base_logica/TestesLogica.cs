﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestesLogica : MonoBehaviour
{
    Tabuleiro tabuleiro;
    //CLASSE PURAMENTE PARA TESTES, ADICIONE O PREFAB TestesLogica a cena para ver o output no console
    void Start() //trate como o main
    {

        testesIA();

        //MaoUsuario.insereMaoInicial(deck.pegaCartasIniciais());
        

       
        /*Debug.Log("VAI MOSTRAR A LISTA DE TODAS AS JOGADAS POSSIVEIS\n\n");
        foreach (ArrayList al in grupos)
        {
            Debug.Log("----------------------------------Jogada Possível-------------------------------------\n");
            foreach (Conjunto c in al)
            {
                Debug.Log("**Inicio de Conjunto**");
                c.printaPecas();
            }
            
        }*/
        
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

    public bool testesIA(){

        Debug.Log("TesteRetornaGrupos1 - Resultado: " + testeRetornaGrupos1());

        return testeRetornaGrupos1();
    }

    public bool testeRetornaGrupos1(){
        
        //Só é possível fazer grupos na mão(Sem Coringas)
        Deck deck = new Deck();
        IA maoIA = new IA();
        
        Peca p1 = new Peca(0,5,false);
        maoIA.inserePeca(p1);
        Peca p2 = new Peca(1,5,false);
        maoIA.inserePeca(p2);
        Peca p3 = new Peca(2,5,false);
        maoIA.inserePeca(p3);
        Peca p4 = new Peca(0,7,false);
        maoIA.inserePeca(p4);
        Peca p5 = new Peca(1,7,false);
        maoIA.inserePeca(p5);
        Peca p6 = new Peca(2,7,false);
        maoIA.inserePeca(p6);
        Peca p7 = new Peca(0,8,false);
        maoIA.inserePeca(p7);
        Peca p8 = new Peca(1,8,false);
        maoIA.inserePeca(p8);
        Peca p9 = new Peca(2,8,false);
        maoIA.inserePeca(p9);


        ArrayList oraculo = new ArrayList();
        ArrayList atual = new ArrayList();
        Conjunto c1 = new Conjunto();
        c1.inserePeca(p1);
        c1.inserePeca(p2);
        c1.inserePeca(p3);
        atual.Add(c1);

        oraculo.Add(atual.Clone());

        Conjunto c2 = new Conjunto();
        c2.inserePeca(p4);
        c2.inserePeca(p5);
        c2.inserePeca(p6);
        atual.Add(c2);

        oraculo.Add(atual.Clone());

        Conjunto c3 = new Conjunto();
        c3.inserePeca(p7);
        c3.inserePeca(p8);
        c3.inserePeca(p9);
        atual.Add(c3);

        oraculo.Add(atual.Clone());
        atual.Remove(c2);
        oraculo.Add(atual.Clone());
        atual.Remove(c1);
        oraculo.Add(atual.Clone());
        atual.Add(c2);
        oraculo.Add(atual.Clone());
        atual.Remove(c3);
        oraculo.Add(atual.Clone());
        

        ArrayList result = maoIA.retornaTodosOsConjuntosDaMao();
        

        //TEM QUE USAR NUGET FLUENTASSERTIONS PARA FUNCIONAR
        return oraculo.Equals(result);

    }
    
}
