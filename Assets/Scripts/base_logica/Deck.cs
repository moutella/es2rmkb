using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    private ArrayList pecas;
    private int quantasPecasTem = 106;
    
    public Deck()
    {
        for(int i=0; i < 4; i++)
        {
            for(int j = 0; j < 13; j++)
            {
                for(int k = 0; k<2; k++)
                {
                    pecas.Add(new Peca(i,j,false));
                }
                
            }
        }
        pecas.Add(new Peca(-1, -1, true));
        pecas.Add(new Peca(-1, -1, true));
    }
    public ArrayList pegaCartasIniciais()
    {
        ArrayList pecasCompradas = new ArrayList();
        for(int i = 0; i<14; i++)
        {
            Peca escolhida = (Peca)pecas[Random.Range(0, quantasPecasTem)];
            pecasCompradas.Add(escolhida);
            pecas.Remove(escolhida);
            quantasPecasTem--;
        }

        return pecasCompradas;
    }
    public Peca pegaPecaAleatoria()
    {
        Peca p = (Peca)pecas[Random.Range(0, quantasPecasTem)];
        pecas.Remove(p);
        return p;
    }
}
