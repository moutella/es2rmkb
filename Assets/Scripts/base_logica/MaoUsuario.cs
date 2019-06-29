using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaoUsuario
{
    protected ArrayList pecas;
    protected ArrayList pecasBackup;
    protected bool primeiraJogada;
    protected bool comprouPeca;

    public MaoUsuario()
    {
        this.pecas = new ArrayList();
        this.primeiraJogada = true;
        this.comprouPeca = false;
    }
    public bool getPrimeiraJogada(){return primeiraJogada;}
    public void setPrimeiraJogada(bool valor){primeiraJogada=valor;}

    public bool getComprouPeca(){return this.comprouPeca;}
    public void setComprouPeca(bool valor){this.comprouPeca = valor;}

    public ArrayList getPecas()
    {
        //this.saveBackupPeca();
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
        IComparer comparador = Peca.getComparadorSequencial();
        pecas.Sort(comparador);
    }

    public void arrumaPorCores(){
        IComparer comparador = Peca.getComparadorPorCores();
        pecas.Sort(comparador);
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
    public int pontuacaoJogada()
    {
        int pontos = 0;
        //ISSO DARÁ ERRADO EM ALGUNS CASOS ENQUANTO NÃO IMPLEMENTAMOS A OPÇÃO EXCLUSIVA ENTRE COMPRAR E JOGAR NA MESA
        foreach(Peca p in pecasBackup)
        {
            if(!this.pecas.Contains(p)){
                Debug.Log("Valor: " +p.getValor()+ " Cor: " + p.getCodigoCor() + " Coringa? " + p.ehCoringa());
                pontos += p.getValor();
            }
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
        //this.jogadaAtual.Clear();
        this.pecasBackup.Clear();
    }
    public void saveBackupPeca() {
        this.pecasBackup = (ArrayList)this.pecas.Clone();
        Debug.Log("Salvou Backup");
    }
    public void rollbackPecas() {
        this.pecas = (ArrayList)this.pecasBackup.Clone();
    }
    public bool jogouAlgumaPeca(){
        return !(this.pecas.Count==this.pecasBackup.Count);
    }
    public bool estavaNaMao(Peca p){
        return this.pecasBackup.Contains(p);
    }
    
}
