


public class MCTSNo{              //Arvore do monte Carlo
    public MCTSNo pai;
    public ArrayList filhos;
    public  Estado estado;
    public int vitorias;
    public int visitas;

    public int UCT;    // Valor do estado

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
        for (int i=0;i<jogadasPossiveis;i++){     //Criando uma copia do estado atual para cada jogada possivel e realizando a jogada
                Estado novoEstado=this.estado.copy();    //Copia o estado atual
                novoEstado.jogar(jogadasPossiveis[i]);   //Faz a jogada na copia do estado atual
                res.Add(new MCTSNo(this,novoEstado,jogadasPossiveis[i]));    //Adiciono um novo filho 
        }
    }

    public Estado selecao(){  //Função que irá selecionar qual o filho será escolhido
        Estado melhor=null;
        foreach(filho p in this.filhos){
            filho.UCT = (filho.vitorias / filho.visitas) + (1.4 * (Math.sqrt(Math.log(this.visitas) / filho.visitas)));
            if(melhor!=null){
                    if(melhor.UCT<filhos.UCT){
                        melhor=filho;
                    }
            }else{
                melhor=filho;
            }
        }
        return melhor;

    }
    public void backPropagation(int vitoria,int visita,Estado estado){   // Função que irá subir na arvore mudando as vitorias e visitas
        if(estado.pai==null){
            return;
        }
        else{
            estado.vitorias+=vitoria;
            estado.visitas+=visitas;
            backPropagation(vitoria,visita,estadoo.pai);
        }
    }
    public void simulacao(Estado estado){     // Função que irá jogar aleatoriamente até a folha.
    	if(estado.filhos==null){
            estado.expansao();
        }
        if(estado.ehEstadoFinal()){
            estado.backPropagation();
        }else{
            Random rnd=new Random();
            int escolhido=rnd.Next(estado.filhos.getLength);
            simulacao(estado.filhos[escolhido]);
        }
    }




}