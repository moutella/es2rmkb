using System;
using System.Collections;

public class Tabuleiro
{
    private ArrayList conjuntos;
    
    public Tabuleiro(){
        this.conjuntos = new ArrayList();
    }

    public void insereConjunto(Conjunto c){
        this.conjuntos.Add(c);
        //this.conjuntos.Sort();
    }

    public void removeConjunto(Conjunto c){
        this.conjuntos.Remove(c);
    }

    public void removeConjunto(int i){
        if(i<this.conjuntos.Count){this.conjuntos.RemoveAt(i);}
    }

    public bool validaTabuleiro(){
      foreach(Conjunto c in this.conjuntos){
        if(!c.getValida()) {
          return false;
        }
      }
      return true;
    }
}