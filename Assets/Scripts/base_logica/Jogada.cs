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

    public int contaPontos(){
        int valor = 0;
        foreach(SubJogada s in subjogadas){
            if(s.tipo==SubJogada.NOVO){
                foreach(Peca p in s.pai.getPecas()){
                    valor+=p.getValor();
                }
            }
        }
        return valor;
    }

    public int contaPecas(){
        int valor = 0;
        foreach(SubJogada s in subjogadas){
            if(s.tipo==SubJogada.NOVO){
                valor+=s.pai.getPecas().Count;
            }else if(s.tipo==SubJogada.INS) valor++;
        }

        return valor;
    }
}
