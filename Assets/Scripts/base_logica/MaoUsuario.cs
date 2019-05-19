using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaoUsuario : MonoBehaviour
{
    private ArrayList pecas;

    public MaoUsuario()
    {
        this.pecas = new ArrayList();
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

}
