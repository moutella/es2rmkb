using System.Collections;
using System.Collections.Generic;

public class Jogador
{
    private MaoUsuario mao;
    private bool primJogada;
    private ArrayList jogadaAtual;

    public Jogador(){
        mao = new MaoUsuario();
        primJogada = true;
        jogadaAtual = new ArrayList();
    }

}
