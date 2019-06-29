using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteLogica
{
    
    [Test]
    public void ValidateSequence()
    {
        Conjunto c = TesteLogicaMocks.mockConjuntoComGrupoValidoSemCoringa();
        bool resp = c.validaSequencia();
        Assert.AreEqual(true, resp);
    }
}
