using System;
using System.Collections;
using UnityEngine;
public class Conjunto
{
    private ArrayList pecas;
    private int tipo; //para salvarmos se a jogada é grupo ou sequencia: -1 inválido, 0 grupo, 1 sequencia
    private bool valida; //flag para sabermos se a jogada é válida[utilizado na checagem final do board]
    private Vector3 pos;

    public Conjunto() {
        this.pecas = new ArrayList();
    }
    public void setPos(Vector3 pos)
    {
        this.pos = pos;
    }

    public Vector3 getPos()
    {
        return pos;
    }
    public int getNumPecas(){
        return this.pecas.Count;
    }
    public bool getValida()
    {
        return this.valida;
    }
    public void inserePeca(Peca p){
        this.pecas.Add(p);

        this.valida = this.validaConjunto();
        //this.pecas.Sort(); quando inserirmos o coringa, ele ainda não terá um valor atribuido
    }
    public void inserePecaAntes(Peca p)
    {
        pecas.Insert(0, p);

        this.valida = this.validaConjunto();
    }

    public void removePeca(Peca p){
        this.pecas.Remove(p);

        this.valida = this.validaConjunto();
    }

    public void insereConjunto(Conjunto conj)
    {
        foreach(Peca p in conj.pecas)
        {
            inserePeca(p);
        }
        this.valida = this.validaConjunto();
    }

    public void removePeca(int i){
        if(i<this.pecas.Count){this.pecas.RemoveAt(i);}
        this.valida = this.validaConjunto();
    }
    public void atualizaCoringa(Peca p, int valor, int cor){
        p.setValor(valor);
        p.setCodigoCor(cor);
    }

    public bool validaConjunto(){
        if(pecas.Count < 3)  return false;
        
        return (this.validaGrupo() || this.validaSequencia());
    }

    public bool validaGrupo(){
        int valorDoGrupo=0;
        int numeroCoringas = 0;
        bool[] cores = new bool[] {false,false,false,false};

        if(this.pecas.Count>4) return false;

        foreach(Peca p in this.pecas){
            if(!p.ehCoringa()){//se atual é coringa não fazemos nada
                if(valorDoGrupo==0){//se atual é o primeiro setamos o valor do grupo
                    valorDoGrupo = p.getValor();
                }else if(p.getValor()!=valorDoGrupo) return false;//senão verificamos se o valor é o mesmo
                if(cores[p.getCodigoCor()]) return false;
                else cores[p.getCodigoCor()] = true;
            }
            else
            {
                numeroCoringas++;
            }
        }
        foreach (Peca p in this.pecas)
        {
            if (p.ehCoringa())
            {
                atualizaCoringa(p, valorDoGrupo, -1);
            }
        }
            this.tipo = 0;
        return true;
    }


    public bool validaSequencia(){
        int numeroCoringas = 0;             
        bool firstvalue = true;                 
        int corDaSequencia = -1;
        int valorAtual = -1;
        foreach (Peca p in this.pecas)
        {
            valorAtual++;
            if (p.ehCoringa())
            {
                numeroCoringas++;
                if(valorAtual > 13)
                {
                    return false;
                }
            }
            else { 
                if (firstvalue)
                {
                    corDaSequencia = p.getCodigoCor();
                    valorAtual = p.getValor();
                    firstvalue = false;
                    if (numeroCoringas >= valorAtual) {
                        return false;
                    }
                }
                else
                {   
                    int valorPeca = p.getValor();
                    if((valorPeca != valorAtual) || (p.getCodigoCor()!=corDaSequencia)||(valorAtual>13))
                    {
                        return false;
                    }
                }
            }
        }
        int valorAtribuido = valorAtual-pecas.Count+1;
        if (numeroCoringas > 0) { 
            foreach (Peca p in this.pecas)
            {
                if (p.ehCoringa())
                {
                    atualizaCoringa(p, valorAtribuido, corDaSequencia);
                }
                valorAtribuido++;
            }
        }
        this.tipo = 1;
        return true;
    }
    public Conjunto cloneConjunto()
    {
        Conjunto clone = new Conjunto();
        foreach(Peca p in this.pecas)
        {
            Peca clonePeca = p.clonePeca();
            clone.inserePeca(p);
        }
        return clone;
    }
    public void printaPecas()
    {
        foreach(Peca p in pecas)
        {
            Debug.Log(p.getValor() + " " + p.getCodigoCor());
        }
    }
    public ArrayList getPecas()
    {
        return pecas;
    }

    public Conjunto divide(Peca p){
        int indP = this.pecas.IndexOf(p);
        Peca atual;

        if(indP==this.pecas.Count-1 || indP==0){
            this.removePeca(indP);
            return null;
        }else {
            Conjunto novo = new Conjunto();
            int diferenca = (this.pecas.Count-1) - indP;
            int n = this.pecas.Count-1;
            for(int i=n;i>indP;i--){
                atual = (Peca) this.pecas[i];
                novo.inserePecaAntes(atual);
                this.removePeca(atual);
            }
            this.removePeca(indP);

            return novo;
        }
    }
    public Vector3 calculaPosPorPecas()
    {
        Vector3 posicao = Vector3.zero;
        //Debug.Log("NUMERO DE ELEMENTOS: " + pecasObjFilho.Count + "NUMERO PECAS: " + conjuntoLogico.getPecas().Count);
        foreach (Peca p in pecas)
        {
        //    Debug.Log(p.getPosition());
            posicao += p.getPosition();

        }
        //Debug.Log(posicao);
        posicao = posicao / pecas.Count;
        Debug.Log(posicao);
        this.pos = posicao;
        return posicao;
    }
    
}