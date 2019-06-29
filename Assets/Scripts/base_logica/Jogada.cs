using System.Collections;
using System.Collections.Generic;

public class Jogada
{
    public ArrayList subjogadas;

    public Jogada()
    {
        subjogadas = new ArrayList();
    }

    public void insereSubJogada(SubJogada s){
        subjogadas.Add(s);
    }

    public void removeSubJogada(SubJogada s){
        subjogadas.Remove(s);
    }

    public Jogada clonaJogada(){
        Jogada clone = new Jogada();
        foreach (SubJogada s in subjogadas)
        {
            clone.insereSubJogada(new SubJogada(s.peca,s.tipo,s.pai,s.inicio,s.dest));
        }
        return clone;
    }
}
