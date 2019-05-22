using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaoUsuario
{
    private ArrayList pecas;
    private ArrayList pecasBackup;
    private bool primeiraJogada;
    private ArrayList jogadaAtual;

    public MaoUsuario()
    {
        this.pecas = new ArrayList();
        this.primeiraJogada = true;
        this.jogadaAtual = new ArrayList();
    }
    public bool getPrimeiraJogada(){return primeiraJogada;}
    public void setPrimeiraJogada(bool valor){primeiraJogada=valor;}

    public ArrayList getPecas()
    {
        this.saveBackupPeca();
        return this.pecas;
    }
    public void compraPeca(Deck deck)
    {
        Peca p = deck.pegaPecaAleatoria();
        inserePeca(p);
    }
    public void insereMaoInicial(ArrayList pecasIniciais)
    {
        foreach(Peca p in pecasIniciais)
        {
            pecas.Add(p);
        }
    }
    public void inserePeca(Peca p)
    {
        if (p.ehCoringa())
        {
            p.setCodigoCor(-1);
            p.setValor(-1);
        }
        this.pecas.Add(p);
        //this.pecas.Sort(); quando inserirmos o coringa, ele ainda não terá um valor atribuido
    }

    public void removePeca(Peca p)
    {
        this.pecas.Remove(p);
    }

    public void arrumaSequencial()
    {
        pecas.Sort();
    }

    public int pontuacao()
    {
        int pontos = 0;
        foreach(Peca p in pecas)
        {
            pontos += p.getPontos();
        }
        return pontos;
    }
    public void printaPecas()
    {
        foreach(Peca p in pecas)
        {
            Debug.Log("Valor: " +p.getValor()+ " Cor: " + p.getCodigoCor() + " Coringa? " + p.ehCoringa());
        }
    }

    public void limpaJogada(){
        this.jogadaAtual.Clear();
    }
    public int pontuacaoJogada()
    {
        int pontos = 0;
        foreach(Peca p in jogadaAtual)
        {
            pontos += p.getPontos();
        }
        return pontos;
    }
    public void saveBackupPeca() {
        this.pecasBackup = (ArrayList)this.pecas.Clone();
    }
    public void rollbackPecas() {
        this.pecas = (ArrayList)this.pecasBackup.Clone();
    }
    public bool jogouPeca(){
        return this.pecas.Count==this.pecasBackup.Count;
    }
    
}
