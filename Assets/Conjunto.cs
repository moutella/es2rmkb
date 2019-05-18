using System;
using System.Collections;
using Peca;

public class Conjunto
{
    private ArrayList pecas;
    //Contains

    public void inserePeca(Peca p){
        pecas.Add(p);
    }

    public void removePeca(Peca p){
        pecas.Remove(Peca p);
    }

    public void removePeca(int i){
        if(i<pecas.Count){pecas.RemoveAt(i);}
    }
}