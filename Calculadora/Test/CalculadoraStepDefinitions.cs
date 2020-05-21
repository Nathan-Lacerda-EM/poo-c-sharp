using Calculadora.Domain;
using System;
using System.Globalization;
using TechTalk.SpecFlow;
using Xunit;

namespace Calculadora.Test
{
    [Binding]
    public class CalculadoraStepDefinitions
    {
        private object valor1, valor2;

        [Given(@"the first int value is (.*)")]
        public void GivenTheFirstIntValueIs(int valor)
        {
            valor1 = valor;
        }

        [Given(@"the second int value is (.*)")]
        public void GivenTheSecondIntValueIs(int valor)
        {
            valor2 = valor;
        }

        [Then(@"the int result should be (.*)")]
        public void ThenTheIntResultShouldBe(int valor)
        {
            Assert.Equal(new Calculadora<int>().Add((int)valor1, (int)valor2), valor);
        }

        [Given(@"the first double value is (.*)")]
        public void GivenTheFirstDoubleValueIs(double valor)
        {
            valor1 = valor;
        }

        [Given(@"the second double value is (.*)")]
        public void GivenTheSecondDoubleValueIs(double valor)
        {
            valor2 = valor;
        }

        [Then(@"the double result should be (.*)")]
        public void ThenTheDoubleResultShouldBe(double valor)
        {
            Assert.Equal(Math.Round(new Calculadora<double>().Add((double)valor1, (double)valor2), 7), valor);
        }

        [Given(@"the first decimal value is (.*)")]
        public void GivenTheFirstDecimalValueIs(decimal valor)
        {
            valor1 = valor;
        }

        [Given(@"the second decimal value is (.*)")]
        public void GivenTheSecondDecimalValueIs(decimal valor)
        {
            valor2 = valor;
        }

        [Then(@"the decimal result should be (.*)")]
        public void ThenTheDecimalResultShouldBe(decimal valor)
        {
            Assert.Equal(new Calculadora<decimal>().Add((decimal)valor1, (decimal)valor2), valor);
        }

        [Given(@"the first string value is '([^']*)'")]
        public void GivenTheFirstStringValueIs(string valor)
        {
            valor1 = valor;
        }

        [Given(@"the second string value is '([^']*)'")]
        public void GivenTheSecondStringValueIs(string valor)
        {
            valor2 = valor;
        }

        [Then(@"the string result should be '([^']*)'")]
        public void ThenTheStringResultShouldBe(string valor)
        {
            Assert.Equal(new Calculadora<string>().Add((string)valor1, (string)valor2), valor);
        }

        [Given(@"the first bool is '([^']*)'")]
        public void GivenTheFirstBoolIs(string valor)
        {
            valor1 = Boolean.Parse(valor);
        }

        [Given(@"the second bool is '([^']*)'")]
        public void GivenTheSecondBoolIs(string valor)
        {
            valor2 = Boolean.Parse(valor);
        }

        [Then(@"the bool result should be an exception")]
        public void ThenTheBoolResultShouldBeAnException()
        {
            var exception = Assert.Throws<FormatException>(() =>
                new Calculadora<bool>().Add((bool)valor1, (bool)valor2));
            Assert.Equal("Não é possivel fazer essa operação com o formato Boolean.", exception.Message);
        }

        [Given(@"the first date is '([^']*)'")]
        public void GivenTheFirstDateIs(string valor)
        {
            valor1 = DateTime.Parse(valor, CultureInfo.GetCultureInfo("pt-BR"));
        }

        [Given(@"the second date is '([^']*)'")]
        public void GivenTheSecondDateIs(string valor)
        {
            valor2 = DateTime.Parse(valor, CultureInfo.GetCultureInfo("pt-BR"));
        }

        [Then(@"the date result should be an exception")]
        public void ThenTheDateResultShouldBeAnException()
        {
            var exception = Assert.Throws<FormatException>(() =>
                new Calculadora<DateTime>().Add((DateTime)valor1, (DateTime)valor2));
            Assert.Equal("Não é possivel fazer essa operação com o formato DateTime.", exception.Message);
        }
    }
}
