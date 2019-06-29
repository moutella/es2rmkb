using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TesteLogicaMocks
{
    
    public static Conjunto mockConjuntoComGrupoValidoSemCoringa() {
        Conjunto c = new Conjunto();
        c.inserePeca(new Peca(0, 1, false));
        c.inserePeca(new Peca(0, 2, false));
        c.inserePeca(new Peca(0, 3, false));
        c.inserePeca(new Peca(0, 4, false));
        c.inserePeca(new Peca(0, 5, false));
        return c;
    }
}
