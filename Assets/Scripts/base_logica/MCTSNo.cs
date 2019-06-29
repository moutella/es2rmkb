using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MCTSNo{              //Arvore do monte Carlo
    public MCTSNo pai;
    public ArrayList filhos;
    public  Estado estado;
    public int vitorias;
    public int visitas;

    public double UCT;    // Valor do estado

    public Jogada jogadaGeradora;            //Jogada que gerou esse estado

    public MCTSNo(MCTSNo pai,Estado estado,Jogada jogadaGeradora){
        this.filhos=null;
        this.pai=pai;
        this.vitorias=0;
        this.visitas=0;
        this.estado=estado;
        this.jogadaGeradora=jogadaGeradora;
        this.UCT=0;
    }

    public void expansao(){
        IA jogadorAtual=estado.jogadorAtual();
        ArrayList jogadasPossiveis=jogadorAtual.retornaJogadasPossiveis();
        ArrayList res=new ArrayList();
        for (int i=0;i<jogadasPossiveis.Count;i++){     //Criando uma copia do estado atual para cada jogada possivel e realizando a jogada
                Estado novoEstado=this.estado.clone();    //Copia o estado atual
                novoEstado.jogar((Jogada)jogadasPossiveis[i]);   //Faz a jogada na copia do estado atual
                res.Add(new MCTSNo(this,novoEstado,(Jogada)jogadasPossiveis[i]));    //Adiciono um novo filho 
        }
        this.filhos=res;
    }

    public MCTSNo selecao(){  //Função que irá selecionar qual o filho será escolhido
        MCTSNo melhor=null;
        //MCTSNo colocado para remover erro de compilação
        foreach(MCTSNo filho in this.filhos){
            filho.UCT = (filho.vitorias / filho.visitas) + (1.4 * (Math.Sqrt(Math.Log(this.visitas) / filho.visitas)));
            if(melhor!=null){
                    if(melhor.UCT<filho.UCT){
                        melhor=filho;
                    }
            }else{
                melhor=filho;
            }
        }
        return melhor;

    }

    //Estado trocado por MCTSNo - erros de compilação
    public void backPropagation(int vitoria,int visita,MCTSNo estado){   // Função que irá subir na arvore mudando as vitorias e visitas
        if(estado.pai==null){
            return;
        }
        else{
            estado.vitorias+=vitoria;
            estado.visitas+=visitas;
            backPropagation(vitoria,visita,estado.pai);
        }
    }
    //Estado trocado por MCTSNo - erros de compilação
    public void simulacao(MCTSNo no){     // Função que irá jogar aleatoriamente até a folha.
    	if(no.filhos==null){
            no.expansao();
        }
        if(no.estado.ehEstadoFinal()){
            //Alterado por erros de compilação
            no.backPropagation(no.vitorias, no.visitas, no);
        }else{
            System.Random rnd=new System.Random();
            int escolhido=rnd.Next(no.filhos.Count);
            simulacao((MCTSNo)no.filhos[escolhido]);
        }
    }
    public Boolean foiExpandido(){
        if(this.filhos==null){
            return false;
        }else{
            return true;
        }
    }




}