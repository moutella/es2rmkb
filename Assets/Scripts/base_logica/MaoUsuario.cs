using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaoUsuario
{
    private ArrayList pecas;

    public MaoUsuario()
    {
        this.pecas = new ArrayList();
    }
    public ArrayList getPecas()
    {
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

}
