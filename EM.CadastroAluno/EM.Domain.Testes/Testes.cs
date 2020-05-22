using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Xunit;
using static EM.Domain.Utils;

namespace EM.Domain.Testes
{
    public class Testes
    {
        private Aluno aluno = new Aluno();

        /*
         * TESTE MATRÍCULA
         */

        [Fact(DisplayName = "Teste Set Matrícula")]
        public void SetMatricula_NaoRespeitaTamanho_RetornaFormatException()
        {
            var exception = Assert.Throws<ValidationException>(() => aluno.Matricula = 1234567891);
            Assert.Equal("Tamanho de matrícula excede o máximo (9).", exception.Message);
        }

        /*
         * TESTE NOME
         */

        [Fact(DisplayName = "Teste Set Nome Vazio")]
        public void SetNome_NomeVazio_RetornaValidationException()
        {
            var exception = Assert.Throws<ValidationException>(() => aluno.Nome = null);
            Assert.Equal("O nome deve ter pelo menos um caractere.", exception.Message);
        }

        [Fact(DisplayName = "Teste Set Nome maior que 100")]
        public void SetNome_NomeMaiorQue100_RetornaValidationException()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0;i <= 10;i++)
                stringBuilder.Append("char++++++");
            var exception = Assert.Throws<ValidationException>(() => aluno.Nome = stringBuilder.ToString());
            Assert.Equal("Tamanho de nome deve ser menor ou igual a 100 caracteres.", exception.Message);
        }

        [Fact(DisplayName = "Teste Set Nome menor que 1")]
        public void SetNome_NomeMenorQue1_RetornaValidationException()
        {
            var exception = Assert.Throws<ValidationException>(() => aluno.Nome = "");
            Assert.Equal("Tamanho de nome deve ser maior ou igual a 1.", exception.Message);
        }

        /*
         * FIM TESTE NOME
         */

        /*
         * TESTE NASCIMENTO
         */

        [Fact(DisplayName = "Teste Set Nascimento")]
        public void SetNascimento_NaoEhDataValida_RetornaValidationException()
        {
            var exception = Assert.Throws<ValidationException>(() => aluno.Nascimento = new DateTime(2021, 07, 05));
            Assert.Equal("Data deve ser anterior ao dia de hoje!", exception.Message);
        }

        /*
         * TESTE CPF
         */

        [Theory(DisplayName = "Teste Set CPF inválido")]
        [InlineData("321.712.610-05")]
        [InlineData("123.321.321-44")]
        [InlineData("451.111.222-33")]
        [InlineData("000.111.111-01")]
        public void SetCPFInvalido(string CPF)
        {
            var exception = Assert.Throws<ValidationException>(() => aluno.CPF = CPF);
            Assert.Equal("CPF inválido!", exception.Message);
        }

        [Theory(DisplayName = "Teste Set CPF apenas número inválido")]
        [InlineData("32171261005")]
        [InlineData("12332132144")]
        [InlineData("45111122233")]
        [InlineData("00011111101")]
        public void SetCPFApenasNumeroInvalido(string CPF)
        {
            var exception = Assert.Throws<ValidationException>(() => aluno.CPF = CPF);
            Assert.Equal("CPF inválido!", exception.Message);
        }

        [Theory(DisplayName = "Teste Set & Get CPF")]
        [InlineData("48975163075")]
        [InlineData("12645010059")]
        [InlineData("508.645.970-29")]
        [InlineData("200.219.970-12")]
        [InlineData("981.283660-84")]
        public void GetCPF(string CPF)
        {
            aluno.CPF = CPF;
            Assert.Equal(aluno.CPF, FormatarCPF(CPF));
        }

        /*
         * FIM TESTE CPF
         */

        /*
         * TESTE ToString()
         */

        [Fact(DisplayName = "Teste ToString()")]
        public void MetodoToString()
        {
            aluno.Matricula = 201800774;
            aluno.Nome = "Nathan Lacerda";
            aluno.CPF = "489751.63075";
            aluno.Nascimento = new DateTime(1999, 7, 5);
            aluno.Sexo = EnumeradorDeSexo.Masculino;
            Assert.Equal("[Matrícula: 201800774], [Nome: \"Nathan Lacerda\"], " +
            $"[CPF: 489.751.630-75], [Nascimento: 05/07/1999],  [Sexo: Masculino]", aluno.ToString());
        }

        /*
         * TESTE Equals()
         */

        [Fact(DisplayName = "Teste Equals()")]
        public void MetodoEquals()
        {
            aluno.Matricula = 201800774;
            aluno.Nome = "Nathan Lacerda";
            aluno.CPF = "489.751.630-75";
            aluno.Nascimento = new DateTime(1999, 7, 5);
            aluno.Sexo = EnumeradorDeSexo.Masculino;
            Aluno newAluno = new Aluno(201800774, "Nathan Lacerda", "48975163075",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino);
            Assert.True(aluno.Equals(newAluno) && newAluno.Equals(aluno));
        }
    }
}
