using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteLogicaMocks
{
    
    public Conjunto conjuntoComMaisDe5Pecas() {
        Conjunto c = new Conjunto();
        c.inserePeca(new Peca(0, 1, false));
        c.inserePeca(new Peca(1, 1, false));
        c.inserePeca(new Peca(2, 1, false));
        c.inserePeca(new Peca(3, 1, false));
        c.inserePeca(new Peca(4, 1, false));
        return c;
    }

    public Conjunto conjuntoVazio() {
        Conjunto c = new Conjunto();
        return c;
    }

    public Conjunto conjuntoComMesmoValorECoresDistintasEComCoringa() {
        Conjunto c = new Conjunto();
        c.inserePeca(new Peca(0, 1, false));
        c.inserePeca(new Peca(1, 1, false));
        c.inserePeca(new Peca(2, 1, false));
        c.inserePeca(new Peca(0, 0, true));
        return c;
    }

    public Conjunto conjuntoComUmValorDistintoECoresDiferentes() {
        Conjunto c = new Conjunto();
        c.inserePeca(new Peca(0, 1, false));
        c.inserePeca(new Peca(1, 1, false));
        c.inserePeca(new Peca(2, 2, false));
        c.inserePeca(new Peca(3, 1, false));
        return c;
    }
}
