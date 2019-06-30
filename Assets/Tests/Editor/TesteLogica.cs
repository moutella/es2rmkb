using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteLogica
{
    public TesteLogicaMocks mocks() {
        return new TesteLogicaMocks();
    }

    // Input: Conjunto maior que 4 peças
    // Output: Falso, já que temos apenas 4 cores diferentes.
    [Test]
    public void TesteValidaGrupo01()
    {
        Conjunto c = this.mocks().conjuntoComMaisDe5Pecas();
        bool resposta = c.validaGrupo();
        Assert.AreEqual(false, resposta);
    }


    // Input: Conjunto vazio
    // Output: Falso, já que um conjunto vazio é inválido
    [Test]
    public void TesteValidaGrupo02()
    {
        Conjunto c = this.mocks().conjuntoVazio();
        bool resposta = c.validaGrupo();
        Assert.AreEqual(false, resposta);
    }


    // Input: Grupo valido, com um coringa
    // Output: Verdadeiro
    [Test]
    public void TesteValidaGrupo03()
    {
        Conjunto c = this.mocks().conjuntoComMesmoValorECoresDistintasEComCoringa();
        bool resposta = c.validaGrupo();
        Assert.AreEqual(true, resposta);
    }


    // Input: Conjunto que so nao é grupo porque tem um valor distinto
    // Output: Falso
    [Test]
    public void TesteValidaGrupo04()
    {
        Conjunto c = this.mocks().conjuntoComUmValorDistintoECoresDiferentes();
        bool resposta = c.validaGrupo();
        Assert.AreEqual(false, resposta);
    }

    // Input: Um conjunto com ao menos uma peça com a mesma cor
    // Output: Falso
    [Test]
    public void TesteValidaGrupo05()
    {
        Conjunto c = this.mocks().conjuntoComMesmoValoreEUmaCorIgual();
        bool resposta = c.validaGrupo();
        Assert.AreEqual(false, resposta);
    }

    // Input: Um conjunto que de valores iguais, com cores distintas e sem coringa
    // Output: Verdadeiro
    [Test]
    public void TesteValidaGrupo06()
    {
        Conjunto c = this.mocks().conjuntoComMesmoValoreEUmaCoresDistintas();
        bool resposta = c.validaGrupo();
        Assert.AreEqual(true, resposta);
    }
}
