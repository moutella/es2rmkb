using System;
using System.Collections;

public class Conjunto
{
    private ArrayList pecas;


    public Conjunto(){
        pecas = new ArrayList();
    }
    
    public void inserePeca(Peca p){
        pecas.Add(p);
    }

    public void removePeca(Peca p){
        pecas.Remove(p);
    }

    public void removePeca(int i){
        if(i<pecas.Count){pecas.RemoveAt(i);}
    }
}