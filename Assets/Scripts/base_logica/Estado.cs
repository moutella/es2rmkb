using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Estado{   //Precisa criar um estado que armazena a mão os dois jogadores e o tabuleiro
    public IA jogadorIa;
    public IA jogadorRandom;
    public Tabuleiro tabuleiro;
    public Deck deck;




    public Estado(IA jogadorIA,IA jogadorRandom,Tabuleiro tabuleiro,Deck deck){
        this.jogadorIa=jogadorIA;
        this.jogadorRandom=jogadorRandom;
        this.tabuleiro=tabuleiro;
        this.deck=deck;
    }

    public IA jogadorAtual(){
        if(this.jogadorIa.jogadorAtual){
            return this.jogadorIa;
        }else{
            return this.jogadorRandom;
        }
        //TODO: Função que retorna o jogador atual
    }
    public void jogar(Jogada jogada){
        jogadorAtual().jogar(jogada,this.tabuleiro, this.jogadorAtual());
        mudarTurno();
    }
    public Estado clone(){
        Estado newEstado=new Estado(this.jogadorIa.clone(),this.jogadorRandom.clone(),this.deck.clone());
        return null;
    }
    public void mudarTurno(){
        if(jogadorIa.jogadorAtual){
            jogadorIa.jogadorAtual=false;
            jogadorRandom.jogadorAtual=true;
        }else{
            jogadorIa.jogadorAtual=true;
            jogadorRandom.jogadorAtual=false;

        }
    }

    public bool ehEstadoFinal(){    ///função que diz se o estado chegou ao seu ponto final(Vitoria ou derrota)
        return false;
    }

}