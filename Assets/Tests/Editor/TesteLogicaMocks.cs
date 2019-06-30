using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TesteLogicaMocks
{
    
    public static Conjunto conjuntoComMaisDe5Pecas() {
        Conjunto c = new Conjunto();
        c.inserePeca(new Peca(0, 1, false));
        c.inserePeca(new Peca(1, 1, false));
        c.inserePeca(new Peca(2, 1, false));
        c.inserePeca(new Peca(3, 1, false));
        c.inserePeca(new Peca(4, 1, false));
        return c;
    }
}
