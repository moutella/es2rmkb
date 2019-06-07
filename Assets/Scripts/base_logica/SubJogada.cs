using System.Collections;
using System.Collections.Generic;

public class SubJogada
{
    public const int SPLIT = 0;
    public const int INS = 1;
    public const int MOVE = 2;
     
    public Peca peca;
    public int tipo;
    public Conjunto pai;
    public Conjunto dest;

    public SubJogada(Peca p, int t, Conjunto pai = null, Conjunto dest = null){
        this.peca = p;
        this.tipo = t;
        this.pai = pai;
        this.dest = dest;
    }
    
}
