using System.Collections;
using System.Collections.Generic;

public class SubJogada
{
    public const int SPLIT = 0;
    public const int INS = 1;
    public const int NOVO = 2;
    public const int MOVE = 3;
     
    public Peca peca;
    public int tipo;
    public Conjunto pai;
    public Conjunto dest;
    public bool inicio;

    public SubJogada(Peca p, int t, Conjunto pai = null, bool ini=false, Conjunto dest = null){
        this.peca = p;
        this.tipo = t;
        this.pai = pai;
        this.dest = dest;
        this.inicio = ini;
    }
    
}
