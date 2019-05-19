using System;
using System.Collections;
using UnityEngine;
public class Conjunto
{
    private ArrayList pecas;
    private int tipo; //para salvarmos se a jogada é grupo ou sequencia: -1 inválido, 0 grupo, 1 sequencia
    private bool valida; //flag para sabermos se a jogada é válida[utilizado na checagem final do board]


    public Conjunto(){
        this.pecas = new ArrayList();
    }

    public void inserePeca(Peca p){
        this.pecas.Add(p);
        this.pecas.Sort();
    }

    public void removePeca(Peca p){
        this.pecas.Remove(p);
    }

    public void removePeca(int i){
        if(i<this.pecas.Count){this.pecas.RemoveAt(i);}
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

        bool[] cores = new bool[] {false,false,false,false};

        if(this.pecas.Count>4) return false;

        foreach(Peca p in this.pecas){
            if(!(p.ehCoringa())){//se atual é coringa não fazemos nada
                if(valorDoGrupo==0){//se atual é o primeiro setamos o valor do grupo
                    valorDoGrupo = p.getValor();
                }else if(p.getValor()!=valorDoGrupo) return false;//senão verificamos se o valor é o mesmo
                if(cores[p.getCodigoCor()]) return false;
                else cores[p.getCodigoCor()] = true;
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
        foreach(Peca p in this.pecas)
        {
            if (p.ehCoringa())
            {
                p.setValor(valorAtual);
                p.setCodigoCor(corDaSequencia);
                numeroCoringas++;
            }
            else { 
                valorAtual++;
                if (firstvalue)
                {
                    corDaSequencia = p.getCodigoCor();
                    valorAtual = p.getValor();
                    firstvalue = false;
                }
                else
                {
                    int valorPeca = p.getValor();
                    if((valorPeca != valorAtual) || (p.getCodigoCor()!=corDaSequencia)||(valorPeca<=numeroCoringas)||(valorAtual>13))
                    {
                        return false;
                    }
                }
            }
        }
        this.tipo = 1;
        return true;
    }

    
}