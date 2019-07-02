using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteLogicaGrupo
{
    public TesteLogicaGrupoMocks mocks() {
        return new TesteLogicaGrupoMocks();
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


    // Input: Conjunto com mesmo valor e cores distintas, contendo também um coringa
    // Output: Verdadeiro
    [Test]
    public void TesteValidaGrupo03()
    {
        Conjunto c = this.mocks().conjuntoComMesmoValorECoresDistintasEComCoringa();
        bool resposta = c.validaGrupo();
        Assert.AreEqual(true, resposta);
    }


    // Input: Conjunto com ao menos um valor distinto
    // Output: Falso
    [Test]
    public void TesteValidaGrupo04()
    {
        Conjunto c = this.mocks().conjuntoComUmValorDistintoECoresDiferentes();
        bool resposta = c.validaGrupo();
        Assert.AreEqual(false, resposta);
    }

    // Input: Um conjunto com valores iguais, mas ao menos uma cor distinta
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
