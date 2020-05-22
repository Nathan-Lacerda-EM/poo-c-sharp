using EM.Domain;
using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace EM.Repository.Testes
{
    [Collection("Database collection")]
    public class Testes
    {

        DatabaseFixture fixture;

        public Testes(DatabaseFixture fixture)
        {
            this.fixture = fixture;
        }

        /*
         * TESTE ADIÇÃO
         * UTILIZANDO aluno
         */

        [Fact(DisplayName = "Adicionar aluno no repositório")]
        public void Adicionar_Um_Aluno_No_Repositorio()
        {
            Assert.True(fixture.repositorio.GetByMatricula(201800774).ElementAt(0).Equals(fixture.aluno));
        }

        [Fact(DisplayName = "Adicionar aluno que já existe no repositório")]
        public void Adicionar_Um_Aluno_Existente_No_Repositorio()
        {
            var exception = Assert.Throws<Exception>(() => fixture.repositorio.Add(fixture.aluno));
            Assert.Equal("Aluno ou CPF já registrado!", exception.Message);
        }

        /*
         * TESTE REMOÇÃO
         * UTILIZANDO aluno1
         */

        [Fact(DisplayName = "Remover aluno do repositório")]
        public void Remover_Um_Aluno_Do_Repositorio()
        {
            fixture.repositorio.Remove(fixture.aluno1);
            Assert.True(fixture.repositorio.Get(aluno => aluno.Equals(fixture.aluno1)).Count() == 0);
        }

        [Fact(DisplayName = "Remover aluno inexistente do repositório")]
        public void Remover_Um_Aluno_Inexistente_Do_Repositorio()
        {
            var exception = Assert.Throws<Exception>(() => fixture.repositorio.Remove(fixture.aluno1));
            Assert.Equal("Aluno não encontrado!", exception.Message);
        }

        /*
         * TESTE ATUALIZAÇÃO
         * UTILIZANDO aluno2 e aluno3
         */

        [Fact(DisplayName = "Atualizar aluno do repositório")]
        public void Atualizar_Aluno_Do_Repositorio()
        {
            fixture.aluno3 = fixture.aluno2;
            fixture.repositorio.Update(fixture.aluno3);
            Assert.True(fixture.aluno3.Equals(fixture.aluno2));
        }

        [Fact(DisplayName = "Atualizar aluno inexistente do repositório")]
        public void Atualizar_Aluno_Inexistente_Do_Repositorio()
        {
            var exception = Assert.Throws<Exception>(() => fixture.repositorio.Update(fixture.aluno1));
            Assert.Equal("Aluno não encontrado!", exception.Message);
        }

        [Fact(DisplayName = "Atualizar aluno com CPF existente no repositório")]
        public void Atualizar_Aluno_Cpf_Existente_Do_Repositorio()
        {
            var exception = Assert.Throws<Exception>(() => fixture.repositorio.Update(fixture.aluno2));
            Assert.Equal("CPF já registrado!", exception.Message);
        }

        /*
         * TESTE MÉTODOS
         * UTILIZANDO aluno2 e aluno3
         */

        [Fact(DisplayName = "Retornar todos os alunos do repositório")]
        public void Retornar_Todos_Os_Alunos_Do_Repositorio()
        {
            Assert.Contains(fixture.repositorio.GetAll().ToString(), fixture.repositorioAuxiliar.GetAll().ToString());
        }

        [Fact(DisplayName = "Retornar todos os alunos do repositório vazio")]
        public void Retornar_Todos_Os_Alunos_Do_Repositorio_Vazio()
        {
            foreach (var aluno in fixture.repositorioAuxiliar.GetAll())
                fixture.repositorioAuxiliar.Remove(aluno);
            var exception = Assert.Throws<Exception>(() => fixture.repositorioAuxiliar.GetAll());
            Assert.Equal("Não existe nenhum aluno no repositório!", exception.Message);
        }

        [Fact(DisplayName = "Retornar todos os alunos do repositório")]
        public void Retornar_Alunos_Do_Repositorio()
        {
            var exception = Assert.Throws<Exception>(() => fixture.repositorioAuxiliar.Get(aluno => aluno.Matricula == 51551));
            Assert.Equal("Esse aluno não existe!", exception.Message);
        }

        [Fact(DisplayName = "Pegar aluno por matrícula do repositório")]
        public void Pega_Aluno_Por_Matricula_Do_Repositorio()
        {
            Assert.Equal(fixture.repositorio.GetByMatricula(201800774).First(), fixture.aluno);
        }

        [Fact(DisplayName = "Pegar aluno por matrícula inexistente no repositório")]
        public void Pega_Aluno_Por_Matricula_Inexistente_No_Repositorio_Vazio()
        {
            Assert.Equal(fixture.repositorio.GetByMatricula(5551).First(), fixture.aluno);
        }

        [Fact(DisplayName = "Pegar aluno por parte do nome no repositório")]
        public void Pega_Aluno_Por_Parte_Nome_No_Repositorio()
        {
            var alunos = fixture.repositorio.GetByContendoNoNome("João");

            Assert.NotEqual(alunos.First(), alunos.Last());
        }
    }

    public class DatabaseFixture : IDisposable
    {
        public Aluno aluno = new Aluno();
        public Aluno aluno1 = new Aluno();
        public Aluno aluno2 = new Aluno();
        public Aluno aluno3 = new Aluno();
        public RepositorioAluno repositorio = new RepositorioAluno();
        public RepositorioAluno repositorioAuxiliar = new RepositorioAluno();

        public DatabaseFixture()
        {
            aluno = new Aluno(201800774, "Nathan Lacerda", "48975163075",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino);
            repositorio.Add(aluno);

            aluno1 = new Aluno(1, "Nathan Lacerda", "640.102.150-03",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino);
            repositorio.Add(aluno1);

            aluno2 = new Aluno(2, "Nathan Lacerda", "060.518.690-18",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino);
            repositorio.Add(aluno2);

            aluno3 = new Aluno(3, "Nathan Lacerda", "060.518.690-18",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino);

            repositorio.Add(new Aluno(4, "Nathan Lacerda João", "071.395.200-89",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino));

            repositorio.Add(new Aluno(5, "Nathan Lacerda João", "912.964.910-21",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino));

            repositorio.Add(new Aluno(6, "Nathan Lacerda", "912.964.910-21",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino));

            repositorioAuxiliar.Add(aluno);
            repositorioAuxiliar.Add(aluno2);
            repositorioAuxiliar.Add(new Aluno(4, "Nathan Lacerda", "071.395.200-89",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino));

            repositorioAuxiliar.Add(new Aluno(5, "Nathan Lacerda", "912.964.910-21",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino));

            repositorioAuxiliar.Add(new Aluno(6, "Nathan Lacerda", "912.964.910-21",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(aluno);
            GC.SuppressFinalize(aluno1);
            GC.SuppressFinalize(aluno2);
            GC.SuppressFinalize(aluno3);
            GC.SuppressFinalize(repositorio);
            GC.SuppressFinalize(repositorioAuxiliar);
        }
    }

    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture> { }
}
