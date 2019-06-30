using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteLogica
{
    
    // Input: Conjunto maior que 4 peças
    // Output: Falso, já que temos apenas 4 cores diferentes.
    [Test]
    public void TesteValidaGrupo01()
    {
        Conjunto c = TesteLogicaMocks.conjuntoComMaisDe5Pecas();
        bool resposta = c.validaSequencia();
        Assert.AreEqual(false, resposta);
    }

    // Input: Conjunto vazio
    // Output: Falso, já que um conjunto vazio é inválido
    [Test]
    public void TesteValidaGrupo02()
    {
        Conjunto c = TesteLogicaMocks.conjuntoVazio();
        bool resposta = c.validaSequencia();
        Assert.AreEqual(false, resposta);
    }

}
