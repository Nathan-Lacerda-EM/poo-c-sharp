using EM.Domain;
using System;
using System.Linq;
using Xunit;

namespace EM.Repository.Testes
{
    [Collection("Database collection")]
    public class RepositoryTestes
    {
        /*
         * TESTE ADI��O
         * UTILIZANDO aluno
         */

        [Fact(DisplayName = "Adicionar aluno no reposit�rio")]
        public void Adicionar_Um_Aluno_No_Repositorio()
        {
            Aluno aluno = new Aluno(201800774, "Nathan Lacerda", "48975163075",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino);
            RepositorioAluno repositorioAluno = new RepositorioAluno();
            repositorioAluno.Add(aluno);

            Assert.True(repositorioAluno.GetByMatricula(201800774).Equals(aluno));
        }

        [Fact(DisplayName = "Adicionar aluno que j� existe no reposit�rio")]
        public void Adicionar_Um_Aluno_Existente_No_Repositorio()
        {
            Aluno aluno = new Aluno(201800774, "Nathan Lacerda", "48975163075",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino);

            RepositorioAluno repositorioAluno = new RepositorioAluno();
            repositorioAluno.Add(aluno);

            Exception exception = Assert.Throws<Exception>(() => repositorioAluno.Add(aluno));
            Assert.Equal("Aluno ou CPF j� registrado!", exception.Message);
        }

        /*
         * TESTE REMO��O
         * UTILIZANDO aluno1 e aluno3
         */

        [Fact(DisplayName = "Remover aluno do reposit�rio")]
        public void Remover_Um_Aluno_Do_Repositorio()
        {
            Aluno aluno = new Aluno(201800774, "Nathan Lacerda", "48975163075",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino);

            RepositorioAluno repositorioAluno = new RepositorioAluno();
            repositorioAluno.Add(aluno);

            Assert.NotNull(repositorioAluno.GetByMatricula(aluno.Matricula));

            repositorioAluno.Remove(aluno);
            Exception exception = Assert.Throws<Exception>(() => repositorioAluno.GetByMatricula(aluno.Matricula));
            Assert.Equal("N�o existe nenhum aluno com essa matr�cula!", exception.Message);
        }

        [Fact(DisplayName = "Remover aluno inexistente do reposit�rio")]
        public void Remover_Um_Aluno_Inexistente_Do_Repositorio()
        {
            Aluno aluno = new Aluno(201800774, "Nathan Lacerda", "48975163075",
                   new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino);

            RepositorioAluno repositorioAluno = new RepositorioAluno();

            Exception exception = Assert.Throws<Exception>(() => repositorioAluno.Remove(aluno));
            Assert.Equal("Aluno n�o encontrado!", exception.Message);
        }

        /*
         * TESTE ATUALIZA��O
         * UTILIZANDO aluno2 e aluno3
         */

        [Fact(DisplayName = "Atualizar aluno do reposit�rio")]
        public void Atualizar_Aluno_Do_Repositorio()
        {
            Aluno aluno = new Aluno(201800774, "Nathan Lacerda", "48975163075",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino);

            Aluno alunoAux = new Aluno(201800774, "Raimunda Maria", "640.102.150-03",
                new DateTime(1938, 7, 5), EnumeradorDeSexo.Masculino);

            RepositorioAluno repositorioAluno = new RepositorioAluno();
            repositorioAluno.Add(aluno);

            repositorioAluno.Update(alunoAux);

            //TODO: COMPARAR O QUE FOI MUDADO E O EQUALS S� COMPARA A MATR�CULA NESTE OBJETO
            Assert.True(aluno.Equals(alunoAux) &&
                repositorioAluno.GetByContendoNoNome("Raimunda").First().Equals(alunoAux));
        }

        [Fact(DisplayName = "Atualizar aluno inexistente do reposit�rio")]
        public void Atualizar_Aluno_Inexistente_Do_Repositorio()
        {
            Aluno aluno = new Aluno(201800774, "Nathan Lacerda", "48975163075",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino);

            Aluno alunoAux = new Aluno(201800773, "Raimunda Maria", "48975163075",
                new DateTime(1938, 7, 5), EnumeradorDeSexo.Masculino);

            RepositorioAluno repositorioAluno = new RepositorioAluno();
            repositorioAluno.Add(aluno);

            Exception exception = Assert.Throws<Exception>(() => repositorioAluno.Update(alunoAux));
            Assert.Equal("Aluno n�o encontrado!", exception.Message);
        }

        [Fact(DisplayName = "Atualizar aluno com CPF existente no reposit�rio")]
        public void Atualizar_Aluno_Cpf_Existente_Do_Repositorio()
        {
            Aluno aluno = new Aluno(201800774, "Nathan Lacerda", "48975163075",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino);

            Aluno alunoAux = new Aluno(201800773, "Raimunda Maria", "640.102.150-03",
                new DateTime(1938, 7, 5), EnumeradorDeSexo.Masculino);

            RepositorioAluno repositorioAluno = new RepositorioAluno();
            repositorioAluno.Add(aluno);

            repositorioAluno.Add(alunoAux);
            alunoAux.CPF = "48975163075";

            Exception exception = Assert.Throws<Exception>(() => repositorioAluno.Update(alunoAux));
            Assert.Equal("CPF j� registrado!", exception.Message);
        }

        /*
         * TESTE M�TODOS
         * UTILIZANDO aluno2 e aluno3
         */

        [Fact(DisplayName = "Retornar todos os alunos do reposit�rio")]
        public void Retornar_Todos_Os_Alunos_Do_Repositorio()
        {
            Aluno aluno = new Aluno(201800774, "Nathan Lacerda", "48975163075",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino);

            Aluno alunoAux = new Aluno(201800773, "Raimunda Maria", "640.102.150-03",
                new DateTime(1938, 7, 5), EnumeradorDeSexo.Masculino);

            RepositorioAluno repositorioAluno = new RepositorioAluno();
            repositorioAluno.Add(aluno);
            repositorioAluno.Add(alunoAux);

            RepositorioAluno repositorioAlunoAux = new RepositorioAluno();
            repositorioAlunoAux.Add(aluno);
            repositorioAlunoAux.Add(alunoAux);

            Assert.Contains(repositorioAluno.GetAll().ToString(), repositorioAlunoAux.GetAll().ToString());
        }

        [Fact(DisplayName = "Retornar todos os alunos do reposit�rio vazio")]
        public void Retornar_Todos_Os_Alunos_Do_Repositorio_Vazio()
        {
            RepositorioAluno repositorioVazio = new RepositorioAluno();
            Exception exception = Assert.Throws<Exception>(() => repositorioVazio.GetAll());
            Assert.Equal("N�o existe nenhum aluno no reposit�rio!", exception.Message);
        }

        [Fact(DisplayName = "Retornar aluno do reposit�rio")]
        public void Retornar_Aluno_Do_Repositorio()
        {
            Aluno aluno = new Aluno(201800774, "Nathan Lacerda", "48975163075",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino);

            Aluno alunoAux = new Aluno(201800773, "Raimunda Maria", "640.102.150-03",
                new DateTime(1938, 7, 5), EnumeradorDeSexo.Masculino);

            RepositorioAluno repositorioAluno = new RepositorioAluno();
            repositorioAluno.Add(aluno);
            repositorioAluno.Add(alunoAux);

            System.Collections.Generic.IEnumerable<Aluno> alunos = repositorioAluno.Get(aluno => aluno.Matricula == 201800774 || aluno.Nome.Contains("Maria"));

            Assert.True(alunos.Contains(aluno) && alunos.Contains(alunoAux));
        }

        [Fact(DisplayName = "Pegar aluno por matr�cula do reposit�rio")]
        public void Pega_Aluno_Por_Matricula_Do_Repositorio()
        {
            Aluno aluno = new Aluno(201800774, "Nathan Lacerda", "48975163075",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino);

            RepositorioAluno repositorioAluno = new RepositorioAluno();
            repositorioAluno.Add(aluno);

            Assert.Equal(repositorioAluno.GetByMatricula(201800774), aluno);
        }

        [Fact(DisplayName = "Pegar aluno por matr�cula inexistente no reposit�rio")]
        public void Pega_Aluno_Por_Matricula_Inexistente_No_Repositorio_Vazio()
        {
            Aluno aluno = new Aluno(201800774, "Nathan Lacerda", "48975163075",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino);

            RepositorioAluno repositorioAluno = new RepositorioAluno();
            repositorioAluno.Add(aluno);

            Exception exception = Assert.Throws<Exception>(() => repositorioAluno.GetByMatricula(5551));
            Assert.Equal("N�o existe nenhum aluno com essa matr�cula!", exception.Message);
        }

        [Fact(DisplayName = "Pegar aluno por parte do nome no reposit�rio")]
        public void Pega_Aluno_Por_Parte_Nome_No_Repositorio()
        {
            Aluno aluno = new Aluno(201800774, "Nathan Lacerda Pereira da Silva Nunes", "48975163075",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino);

            RepositorioAluno repositorioAluno = new RepositorioAluno();
            repositorioAluno.Add(aluno);
            repositorioAluno.Add(new Aluno(201800773, "Nathan Lacerda Pereira da Silva Nunes", "640.102.150-03",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino));
            repositorioAluno.Add(new Aluno(201800772, "Nathan Lacerda Pereira Nunes", "071.395.200-89",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino));

            System.Collections.Generic.IEnumerable<Aluno> alunos = repositorioAluno.Get(a => a.Nome.Contains("Silva"));

            Assert.True(alunos.ToArray().Length == 2);
        }

        [Fact(DisplayName = "Pegar aluno inexistente por parte do nome no reposit�rio")]
        public void Pega_Aluno_Inexistente_Por_Parte_Nome_No_Repositorio()
        {
            Aluno aluno = new Aluno(201800774, "Nathan Lacerda Pereira da Silva Nunes", "48975163075",
                   new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino);

            RepositorioAluno repositorioAluno = new RepositorioAluno();
            repositorioAluno.Add(aluno);
            repositorioAluno.Add(new Aluno(201800773, "Nathan Lacerda Pereira da Silva Nunes", "640.102.150-03",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino));
            repositorioAluno.Add(new Aluno(201800772, "Nathan Lacerda Pereira Nunes", "071.395.200-89",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino));

            System.Collections.Generic.IEnumerable<Aluno> alunos = repositorioAluno.Get(a => a.Nome.Contains("Silva"));

            Exception exception = Assert.Throws<Exception>(() => repositorioAluno.GetByContendoNoNome("Jos�"));
            Assert.Equal("N�o existe nenhum aluno com esse nome!", exception.Message);
        }
    }

    /*
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

            repositorio.Add(new Aluno(4, "Nathan Lacerda Jo�o", "071.395.200-89",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino));

            repositorio.Add(new Aluno(5, "Nathan Lacerda Jo�o", "912.964.910-21",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino));

            repositorio.Add(new Aluno(6, "Nathan Lacerda", "",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino));

            repositorioAuxiliar.Add(aluno);
            repositorioAuxiliar.Add(aluno2);
            repositorioAuxiliar.Add(new Aluno(4, "Nathan Lacerda Jo�o", "071.395.200-89",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino));

            repositorioAuxiliar.Add(new Aluno(5, "Nathan Lacerda Jo�o", "912.964.910-21",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino));

            repositorioAuxiliar.Add(new Aluno(6, "Nathan Lacerda", "",
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
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture> { }*/
}
