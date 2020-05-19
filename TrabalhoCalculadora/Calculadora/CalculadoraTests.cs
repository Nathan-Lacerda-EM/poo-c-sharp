﻿using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Calculadora.Calculadora
{
    /*
     * Nesta classe de testes implementei o MemberData e o ClassData para fins de aprendizado, porém acredito
     * que esses são melhores para organizar testes maiores, já que o InlineData é mais direto e pode "poluir"
     * o código, nos casos de muitos testes e muita data para que serão inseridos no teste para executá-lo.
     * 
     * Padrão nos métodos utilizado: Método a ser testado_Informação Sobre o Cenário do Teste_O resultado esperado.
     * Exemplo: Soma_DoisInteiros_ResultaInteiroErrado - Este teste vai testar a função Add da Calculadora com
     * dois números inteiros e retornar um inteiro errado, ou seja, se 2 + 2 é 7 o Assert deve ser NotEqual, já
     * que isso é falso.
     * 
     * Aprendizados: Testes com xUnit, InlineData, MemberData, ClassData, IEnumerator, yield return, Padrão de nome dos métodos
     * dos testes, legibilidade e simplificação de código.
     */

    public class CalculadoraTests
    {
        [Fact]
        public void Soma_DoisInteiros_ResultaInteiroErrado() => Assert.NotEqual(7, new Calculadora<int>().Add(2, 2));

        [Fact]
        public void Soma_DuasStrings_ResultaStringErrada() => Assert.NotEqual("ab cd", new Calculadora<string>().Add("ab", "cd"));

        [Fact]
        public void Soma_DoisDecimas_ResultaDecimalErrado() => Assert.NotEqual(4.1M, new Calculadora<decimal>().Add(2M, 2M));

        [Fact]
        public void Soma_DoisDouble_ResultadoDoubleErrado() => Assert.NotEqual(5, new Calculadora<int>().Add(2, 2));

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(-4, -6, -10)]
        [InlineData(-2, 2, 0)]
        [InlineData(int.MinValue, -1, int.MaxValue)]
        [InlineData(46851415, 46851415, 93702830)]
        public void Soma_DoisInteiros_RetornaNumeroInteiro(int valor1, int valor2, int esperado) => 
            Assert.Equal(esperado, new Calculadora<int>().Add(valor1, valor2));

        [Theory]
        [InlineData("ab", "cd", "abcd")]
        [InlineData("4567890", "", "4567890")]
        [InlineData(" ", " ", "  ")]
        [InlineData("Escolar ", "Manager", "Escolar Manager")]
        [InlineData("Nathan ", " Guedes", "Nathan  Guedes")]
        public void Soma_DuasStrings_RetornaString(string valor1, string valor2, string esperado) => 
            Assert.Equal(esperado, new Calculadora<string>().Add(valor1, valor2));

        [Theory]
        [ClassData(typeof(CalculadoraTestsData))]
        public void Soma_DoisDecimais_RetornaDecimal(decimal valor1, decimal valor2, decimal esperado) => 
            Assert.Equal(esperado, new Calculadora<decimal>().Add(valor1, valor2), 1);

        [Theory]
        [MemberData(nameof(Data))]
        public void Soma_DoisDoubles_RetornaDoubles(double valor1, double valor2, double esperado) => 
            Assert.Equal(esperado, new Calculadora<double>().Add(valor1, valor2), 1);

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        [InlineData(true, false)]
        public void Soma_DoisBools_RetornaFormatException(bool valor1, bool valor2) => 
            Assert.Throws<FormatException>(() => new Calculadora<bool>().Add(valor1, valor2));

        /* Dados que serão inseridos no teste utilizando o MemberData. */
        public static IEnumerable<object[]> Data => new List<object[]>
        {
            new object[] { 1.33, 2.57, 3.9 },
            new object[] { -4.2, -6.2, -10.4 },
            new object[] { -2.61, 2.21, -0.4 },
        };
    }

    /* Mais dados que serão inseridos em um teste, porém aqui em classe diferente, já que utilizarei o ClassData. */
    public class CalculadoraTestsData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 623256498494612M, 623256498494612M, 1246512996989224 };
            yield return new object[] { 0.39999999999M, 45120452.65221458M, 45120453.05221458 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}