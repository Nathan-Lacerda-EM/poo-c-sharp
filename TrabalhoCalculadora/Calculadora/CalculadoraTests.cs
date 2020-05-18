using System;
using System.Collections.Generic;
using Xunit;

namespace Trabalho.Calculadora
{
    public class CalculadoraTests
    {
        [Fact]
        public void PassingTest() => Assert.Equal(4, new Calculadora<int>.Calcular(new Calculadora<int>().Add)(2, 2));

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(-4, -6, -10)]
        [InlineData(-2, 2, 0)]
        [InlineData(int.MinValue, -1, int.MaxValue)]
        [InlineData(46851415, 46851415, 93702830)]
        public void TesteDeSomaInteiro(int valor1, int valor2, int esperado) => Assert.Equal(esperado, new Calculadora<int>.Calcular(new Calculadora<int>().Add)(valor1, valor2));

        [Theory]
        [ClassData()]
        public void TesteDeSomaDecimal(decimal valor1, decimal valor2, decimal esperado) => Assert.Equal(esperado, new Calculadora<decimal>.Calcular(new Calculadora<decimal>().Add)(valor1, valor2));

        [Theory]
        [MemberData(nameof(Data))]
        public void TesteDeSomaDouble(double valor1, double valor2, double esperado) => Assert.Equal(esperado, new Calculadora<double>.Calcular(new Calculadora<double>().Add)(valor1, valor2), 1); //O ", 1" é a precisão.

        public static IEnumerable<object[]> Data => new List<object[]>
        {
            new object[] { 1.33, 2.57, 3.9 },
            new object[] { -4.2, -6.2, -10.4 },
            new object[] { -2.61, 2.21, -0.4 },
        };
    }

    public class CalculadoraTestsData
    {

    }
}