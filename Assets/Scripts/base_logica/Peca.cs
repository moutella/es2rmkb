using System;
using System.Collections;
using UnityEngine;
public class Peca : IComparable
{
    private int codigoCor;
    private int valor;
    private bool coringa;
    private Vector3 pos;
    
    public Peca(int cor, int valor, bool coringa)
    {
        this.codigoCor = cor;
        this.valor = valor;
        this.coringa = coringa;
    }


    public int getCodigoCor() { return this.codigoCor; }
    public void setCodigoCor(int cor) { this.codigoCor = cor; }

    public int getValor() { return this.valor; }
    public void setValor(int value) { this.valor = value; }

    public Vector3 getPosition() { return this.pos; }
    public void setPosition(Vector3 pos) { this.pos = pos; }    

    public int getPontos() //Penalidade por ter a peça na mão ao final do jogo
    {
        if (coringa) return 30;
        return valor;
    }

    public class ComparadorPorCores : IComparer
    {
        int IComparer.Compare(object a, object b)
         {
            Peca p1=(Peca)a;
            Peca p2=(Peca)b;

            Debug.Log("Peça " + p1.getValor()+"/"+p1.getCodigoCor() + " comparada com" + "Peça " + p2.getValor()+"/"+p2.getCodigoCor());

            if (p1.getCodigoCor() > p2.getCodigoCor()){
                Debug.Log("Ganhou p1");
                return 1;
            }

            if (p1.getCodigoCor() < p2.getCodigoCor()){
                Debug.Log("Ganhou p2");
                return -1;
            }
            else
                return p1.CompareTo(p2);
                
         }
    }

    public static IComparer getComparadorPorCores(){
        return (IComparer) new ComparadorPorCores();
    }

    public int CompareTo(object obj)
    {
        if (obj == null) return 1;

        Peca p = obj as Peca;
        if (p != null)
        {
            return this.valor.CompareTo(p.valor);
        }
        else
            throw new ArgumentException("Objeto comparado não é uma Peça");

    }



    public Peca clonePeca()
    {
        Peca clone = new Peca(this.codigoCor, this.valor, this.coringa);
        clone.setPosition(pos);
        return clone;
    }
    public bool ehCoringa() { return coringa; }
}