


public class MCTSNo{              //Arvore do monte Carlo
    public MCTSNo pai;
    public ArrayList filhos;
    public  Estado estado;
    public int vitorias;
    public int visitas;

    public Jogada jogadaGeradora;            //Jogada que gerou esse estado

    public MCTSNo(MCTSNo pai,Estado estado,Jogada jogadaGeradora){
        this.pai=pai;
        this.vitorias=0;
        this.visitas=0;
        this.estado=estado;
        this.jogadaGeradora=jogadaGeradora;
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

    }
    public void backPropagation(){   // Função que irá subir na arvore mudando as vitorias e visitas

    }
    public Estado simulacao(){     // Função que ira retornar o melhor estado/jogada(Monte Carlo em si)

    }




}