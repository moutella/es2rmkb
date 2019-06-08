



public class Estado{   //Precisa criar um estado que armazena a mão os dois jogadores e o tabuleiro
    public IA jogadorIa;
    public IA jogadorRandom;
    public Tabuleiro tabuleiro;



    public Estado(MaoIA jogadorIA,MaoIA jogadorRandom,Tabuleiro tabuleiro){
        this.jogadorIa=jogadorIA;
        this.jogadorRandom=jogadorRandom;
        this.tabuleiro=tabuleiro;
    }

    public IA jogadorAtual(){
        if(this.jogadorIa.jogadorAtual){
            return this.jogadorIa;
        }else{
            return this.jogadorRandom;
        }
        //TODO: Função que retorna o jogador atual
    }
    public void jogar(ArrayList subJogadas){
        jogadorAtual().jogar(subJogadas,this.tabuleiro);
        mudarTurno();
    }
    public Estado copy(){
        // TODO: função retornara a copia do estado atual
    }
    public void mudarTurno(){
        if(jogadorIa.jogadorAtual){
            jogadorIa.jogadorAtual=false;
            jogadorRandom.jogadorAtual=true;
        }else{
            jogadorIa.jogadorAtual=true;
            jogadorRandom.jogadorAtual=false;

        }
    }

}