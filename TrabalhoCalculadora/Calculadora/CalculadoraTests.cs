using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Trabalho.Calculadora
{
    public class CalculadoraTests
    {
        [Fact]
        public void Soma_DoisInteiros_ResultaInteiroErrado() => Assert.Equal(4, new Calculadora<int>.Calcular(new Calculadora<int>().Add)(2, 2));

        [Fact]
        public void Soma_DoisDouble_ResultadoDoubleErrado() => Assert.NotEqual(5, new Calculadora<int>.Calcular(new Calculadora<int>().Add)(2, 2));

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(-4, -6, -10)]
        [InlineData(-2, 2, 0)]
        [InlineData(int.MinValue, -1, int.MaxValue)]
        [InlineData(46851415, 46851415, 93702830)]
        public void Soma_DoisInteiros_RetornaNumeroInteiro(int valor1, int valor2, int esperado) => Assert.Equal(esperado, new Calculadora<int>.Calcular(new Calculadora<int>().Add)(valor1, valor2));

        [Theory]
        [InlineData("ab", "cd", "abcd")]
        [InlineData("4567890", "", "4567890")]
        [InlineData(" ", " ", "  ")]
        [InlineData("Escolar ", "Manager", "Escolar Manager")]
        [InlineData("Nathan ", " Guedes", "Nathan  Guedes")]
        public void Soma_DuasStrings_RetornaString(string valor1, string valor2, string esperado) => Assert.Equal(esperado, new Calculadora<string>.Calcular(new Calculadora<string>().Add)(valor1, valor2));

        [Theory]
        [ClassData(typeof(CalculadoraTestsData))]
        public void Soma_DoisDecimais_RetornaDecimal(decimal valor1, decimal valor2, decimal esperado) => Assert.Equal(esperado, new Calculadora<decimal>.Calcular(new Calculadora<decimal>().Add)(valor1, valor2), 1);

        [Theory]
        [MemberData(nameof(Data))]
        public void Soma_DoisDoubles_RetornaDoubles(double valor1, double valor2, double esperado) => Assert.Equal(esperado, new Calculadora<double>.Calcular(new Calculadora<double>().Add)(valor1, valor2), 1);

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        [InlineData(true, false)]
        public void Soma_DoisBools_RetornaFormatException(bool valor1, bool valor2) => Assert.Throws<FormatException>(() => new Calculadora<bool>.Calcular(new Calculadora<bool>().Add)(valor1, valor2));

        [Theory]
        [InlineData(1, 5)]
        [InlineData(2, 6)]
        [InlineData(3, 4)]
        public void Converter_IntEmBool_RetornaFormatException(int valor1, int valor2) { var Calculadora = new Calculadora<int>.Calcular(new Calculadora<int>().Add); Calculadora(valor1, valor2); Assert.Throws<FormatException>(() => Convert.ToBoolean(Calculadora.Target)); }

        public static IEnumerable<object[]> Data => new List<object[]>
        {
            new object[] { 1.33, 2.57, 3.9 },
            new object[] { -4.2, -6.2, -10.4 },
            new object[] { -2.61, 2.21, -0.4 },
        };
    }

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