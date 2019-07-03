using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteLogicaSequenciaMocks
{
    public Conjunto sequenciaCom13PecasMaisUmCoringa() {
        Conjunto c = new Conjunto();
        c.inserePeca(new Peca(1, 1, false));
        c.inserePeca(new Peca(1, 2, false));
        c.inserePeca(new Peca(1, 3, false));
        c.inserePeca(new Peca(1, 4, false));
        c.inserePeca(new Peca(1, 5, false));
        c.inserePeca(new Peca(1, 6, false));
        c.inserePeca(new Peca(1, 7, false));
        c.inserePeca(new Peca(1, 8, false));
        c.inserePeca(new Peca(1, 9, false));
        c.inserePeca(new Peca(1, 10, false));
        c.inserePeca(new Peca(1, 11, false));
        c.inserePeca(new Peca(1, 12, false));
        c.inserePeca(new Peca(1, 13, false));
        c.inserePeca(new Peca(0, 0, true));
        return c;
    }

    public Conjunto conjuntoDeValorEmSequenciaMasUmaCorDiferente() {
        Conjunto c = new Conjunto();
        c.inserePeca(new Peca(1, 1, false));
        c.inserePeca(new Peca(1, 2, false));
        c.inserePeca(new Peca(1, 3, false));
        c.inserePeca(new Peca(1, 4, false));
        c.inserePeca(new Peca(2, 5, false));
        return c;
    }

    public Conjunto conjuntoComUmValorForaDaSequencia() {
        Conjunto c = new Conjunto();
        c.inserePeca(new Peca(1, 1, false));
        c.inserePeca(new Peca(1, 2, false));
        c.inserePeca(new Peca(1, 4, false));
        c.inserePeca(new Peca(1, 5, false));
        return c;
    }

     public Conjunto sequenciaComCoringa() {
        Conjunto c = new Conjunto();
        c.inserePeca(new Peca(1, 1, false));
        c.inserePeca(new Peca(1, 2, false));
        c.inserePeca(new Peca(1, 3, false));
        c.inserePeca(new Peca(1, 1, true));
        return c;
    }

    public Conjunto sequenciaSemCoringa() {
        Conjunto c = new Conjunto();
        c.inserePeca(new Peca(1, 1, false));
        c.inserePeca(new Peca(1, 2, false));
        c.inserePeca(new Peca(1, 3, false));
        c.inserePeca(new Peca(1, 4, false));
        return c;
    }

    public Conjunto conjuntoVazio() {
        Conjunto c = new Conjunto();
        return c;
    }

    public Conjunto intercecaoEntreSerSequenciaESerGrupo() {
        Conjunto c = new Conjunto();
        c.inserePeca(new Peca(1, 1, true));
        c.inserePeca(new Peca(1, 1, true));
        c.inserePeca(new Peca(1, 1, false));
        return c;
    }
}