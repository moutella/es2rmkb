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
    public Tabuleiro cloneTabuleiro()
    {
        Tabuleiro clone = new Tabuleiro();
        foreach(Conjunto c in this.conjuntos)
        {
            Conjunto cloneConj = c.cloneConjunto();
            clone.insereConjunto(cloneConj);
        }
        return clone;
    }

    public void divideJogada(Conjunto c, Peca p){
        Conjunto novo = c.divide(p);

        //Se um novo conjunto foi criado insere no tabuleiro
        if(!Object.ReferenceEquals(novo,null))this.insereConjunto(novo);
        
        //se o conjunto só tinha uma peça e ficou vazio, remove ele do tabuleiro
        if(c.getNumPecas()==0) this.removeConjunto(c);
    }
}