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