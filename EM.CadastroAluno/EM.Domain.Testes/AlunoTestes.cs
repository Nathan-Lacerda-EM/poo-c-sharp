using System;
using System.ComponentModel.DataAnnotations;
using Xunit;
using static EM.Domain.Utils;

namespace EM.Domain.Testes
{
    public class AlunoTestes
    {
        private readonly Aluno aluno = new Aluno();

        /*
         * INÍCIO TESTE MATRÍCULA
         */

        [Fact(DisplayName = "Teste Set Matrícula")]
        public void SetMatricula_NaoRespeitaTamanho_RetornaFormatException()
        {
            var excecaoValidacao = Assert.Throws<ValidationException>(() => aluno.Matricula = 1234567891);
            Assert.Equal("Tamanho de matrícula excede o máximo (9).", excecaoValidacao.Message);
        }

        /*
         * INÍCIO TESTE NOME
         */

        [Fact(DisplayName = "Teste Set Nome Vazio")]
        public void SetNome_NomeVazio_RetornaValidationException()
        {
            var excecaoValidacao = Assert.Throws<ValidationException>(() => aluno.Nome = null);
            Assert.Equal("O nome deve ter pelo menos um caractere.", excecaoValidacao.Message);
        }

        [Fact(DisplayName = "Teste Set Nome maior que 100")]
        public void SetNome_NomeMaiorQue100_RetornaValidationException()
        {
            var excecaoValidacao = Assert.Throws<ValidationException>(() => aluno.Nome = new string('a', 101));
            Assert.Equal("Tamanho de nome deve ser menor ou igual a 100 caracteres.", excecaoValidacao.Message);
        }

        [Fact(DisplayName = "Teste Set Nome menor que 1")]
        public void SetNome_NomeMenorQue1_RetornaValidationException()
        {
            var excecaoValidacao = Assert.Throws<ValidationException>(() => aluno.Nome = "");
            Assert.Equal("Tamanho de nome deve ser maior ou igual a 1.", excecaoValidacao.Message);
        }

        /*
         * FIM TESTE NOME
         *
        
         *
         * INÍCIO TESTE NASCIMENTO
         */

        [Fact(DisplayName = "Teste Set Nascimento data maior que hoje")]
        public void SetNascimentoDataMaior_NaoEhDataValida_RetornaValidationException()
        {
            var excecaoValidacao = Assert.Throws<ValidationException>(() => aluno.Nascimento = DateTime.Now.AddDays(1));
            Assert.Equal("Data deve ser igual ou anterior ao dia de hoje!", excecaoValidacao.Message);
        }

        [Fact(DisplayName = "Teste Set Nascimento ano menor que 1900")]
        public void SetNascimentoDataMenor_NaoEhDataValida_RetornaValidationException()
        {
            var excecaoValidacao = Assert.Throws<ValidationException>(() => aluno.Nascimento = new DateTime(1899, 12, 31));
            Assert.Equal("Ano deve ser maior que 1900!", excecaoValidacao.Message);
        }

        /*
         * FIM TESTE NASCIMENTO
         *
        
         *
         * INÍCIO TESTE CPF
         */

        [Theory(DisplayName = "Teste Set CPF inválido")]
        [InlineData("321.712.610-05")]
        [InlineData("123.321.321-44")]
        [InlineData("451.111.222-33")]
        [InlineData("000.111.111-01")]
        public void SetCPFInvalido(string cpf)
        {
            var excecaoValidacao = Assert.Throws<ValidationException>(() => aluno.CPF = cpf);
            Assert.Equal("CPF inválido!", excecaoValidacao.Message);
        }

        [Theory(DisplayName = "Teste Set CPF apenas número inválido")]
        [InlineData("32171261005")]
        [InlineData("12332132144")]
        [InlineData("45111122233")]
        [InlineData("00011111101")]
        public void SetCPFApenasNumeroInvalido(string cpf)
        {
            var excecaoValidacao = Assert.Throws<ValidationException>(() => aluno.CPF = cpf);
            Assert.Equal("CPF inválido!", excecaoValidacao.Message);
        }

        [Theory(DisplayName = "Teste Set & Get CPF")]
        [InlineData("48975163075")]
        [InlineData("12645010059")]
        [InlineData("508.645.970-29")]
        [InlineData("200.219.970-12")]
        [InlineData("981.283660-84")]
        public void GetCPF(string cpf)
        {
            aluno.CPF = cpf;
            Assert.Equal(aluno.CPF, FormateCPF(cpf));
        }

        /*
         * FIM TESTE CPF
         *

         *
         * TESTE FUNÇÃO ToString()
         */

        [Fact(DisplayName = "Teste da função ToString()")]
        public void MetodoToString()
        {
            aluno.Matricula = 201800774;
            aluno.Nome = "Nathan Lacerda";
            aluno.CPF = "489751.63075";
            aluno.Nascimento = new DateTime(1999, 7, 5);
            aluno.Sexo = EnumeradorDeSexo.Masculino;
            Assert.Equal("[Matrícula: 201800774], [Nome: \"Nathan Lacerda\"], " +
            $"[Sexo: Masculino], [Nascimento: 05/07/1999], [CPF: 489.751.630-75]", aluno.ToString());
        }

        /*
         * TESTE FUNÇÃO Equals()
         */

        [Fact(DisplayName = "Teste da função Equals()")]
        public void MetodoEquals()
        {
            aluno.Matricula = 201800774;
            aluno.Nome = "Nathan Lacerda";
            aluno.CPF = "489.751.630-75";
            aluno.Nascimento = new DateTime(1999, 7, 5);
            aluno.Sexo = EnumeradorDeSexo.Masculino;
            var newAluno = new Aluno(201800774, "Nathan Lacerda", "48975163075",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino);
            Assert.True(aluno.Equals(newAluno) && newAluno.Equals(aluno));
        }
    }
}
