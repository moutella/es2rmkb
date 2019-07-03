using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteLogicaSequencia
{
    public TesteLogicaSequenciaMocks mocks() {
        return new TesteLogicaSequenciaMocks();
    }

    // Input: Conjunto com 13 peças + coringa
    // Output: Falso, porque não tem como ter uma sequencia maior que 13 peças
    [Test]
    public void TesteValidaSequencia01()
    {
        Conjunto c = this.mocks().sequenciaCom13PecasMaisUmCoringa();
        bool resposta = c.validaSequencia();
        Assert.AreEqual(false, resposta);
    }

    // Input: Conjunto com valores em sequência, mas com ao menos um elemento de cor diferente
    // Output: Falso
    [Test]
    public void TesteValidaSequencia02()
    {
        Conjunto c = this.mocks().conjuntoDeValorEmSequenciaMasUmaCorDiferente();
        bool resposta = c.validaSequencia();
        Assert.AreEqual(false, resposta);
    }

    // Input: Conjunto com ao menos um valor fora da sequência
    // Output: Falso
    [Test]
    public void TesteValidaSequencia03()
    {
        Conjunto c = this.mocks().conjuntoComUmValorForaDaSequencia();
        bool resposta = c.validaSequencia();
        Assert.AreEqual(false, resposta);
    }

    // Input: Sequencia com coringa
    // Output: Verdadeiro
    [Test]
    public void TesteValidaSequencia04()
    {
        Conjunto c = this.mocks().sequenciaComCoringa();
        bool resposta = c.validaSequencia();
        Assert.AreEqual(true, resposta);
    }

    // Input: Sequencia sem coringa
    // Output: Verdadeiro
    [Test]
    public void TesteValidaSequencia05()
    {
        Conjunto c = this.mocks().sequenciaSemCoringa();
        bool resposta = c.validaSequencia();
        Assert.AreEqual(true, resposta);
    }

    // Input: Conjunto Vazio
    // Output: Falso
    [Test]
    public void TesteValidaSequencia06()
    {
        Conjunto c = this.mocks().conjuntoVazio();
        bool resposta = c.validaSequencia();
        Assert.AreEqual(false, resposta);
    }

    // Input: Interseção entre ser um coringa e ser uma sequencia
    // Output: Verdadeiro
    [Test]
    public void TesteValidaSequencia07()
    {
        Conjunto c = this.mocks().intercecaoEntreSerSequenciaESerGrupo();
        bool resposta = c.validaSequencia();
        Assert.AreEqual(true, resposta);
    }
}
