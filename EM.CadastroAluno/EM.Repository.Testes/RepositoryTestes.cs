using EM.Domain;
using System;
using System.Linq;
using Xunit;

namespace EM.Repository.Testes
{
    public class RepositoryTestes
    {
        /*
         * INÍCIO TESTE ADIÇÃO
         * UTILIZANDO aluno
         */

        [Fact(DisplayName = "Adicionar aluno no repositório")]
        public void Adicionar_Aluno_No_Repositorio()
        {
            var aluno = new Aluno(201800774, "Nathan Lacerda", "48975163075",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino);
            var repositorioAluno = new RepositorioAluno();
            repositorioAluno.Add(aluno);

            Assert.True(repositorioAluno.GetByMatricula(201800774).Equals(aluno));
        }

        [Fact(DisplayName = "Adicionar aluno que já existe no repositório")]
        public void Adicionar_Aluno_Existente_No_Repositorio()
        {
            var aluno = new Aluno(201800774, "Nathan Lacerda", "48975163075",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino);

            var repositorioAluno = new RepositorioAluno();
            repositorioAluno.Add(aluno);

            var excecao = Assert.Throws<Exception>(() => repositorioAluno.Add(aluno));
            Assert.Equal("Aluno ou CPF já registrado!", excecao.Message);
        }

        /*
         * FIM TESTE ADIÇÃO
         *

         *
         * INÍCIO TESTE REMOÇÃO
         * UTILIZANDO aluno1 e aluno3
         */

        [Fact(DisplayName = "Remover aluno do repositório")]
        public void Remover_Aluno_Do_Repositorio()
        {
            var aluno = new Aluno(201800774, "Nathan Lacerda", "48975163075",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino);

            var repositorioAluno = new RepositorioAluno();
            repositorioAluno.Add(aluno);

            Assert.NotNull(repositorioAluno.GetByMatricula(aluno.Matricula));

            repositorioAluno.Remove(aluno);
            var excecao = Assert.Throws<Exception>(() => repositorioAluno.GetByMatricula(aluno.Matricula));
            Assert.Equal("Não existe nenhum aluno com essa matrícula!", excecao.Message);
        }

        [Fact(DisplayName = "Remover aluno inexistente do repositório")]
        public void Remover_Aluno_Inexistente_Do_Repositorio()
        {
            var aluno = new Aluno(201800774, "Nathan Lacerda", "48975163075",
                   new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino);

            var repositorioAluno = new RepositorioAluno();

            var excecao = Assert.Throws<Exception>(() => repositorioAluno.Remove(aluno));
            Assert.Equal("Aluno não encontrado!", excecao.Message);
        }

        /*
         * FIM TESTE REMOÇÃO
         *

         *
         * INÍCIO TESTE ATUALIZAÇÃO
         * UTILIZANDO aluno2 e aluno3
         */

        [Fact(DisplayName = "Atualizar aluno no repositório")]
        public void Atualizar_Aluno_No_Repositorio()
        {
            var aluno = new Aluno(201800774, "Nathan Lacerda", "48975163075",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino);

            var alunoAux = new Aluno(201800774, "Raimunda Maria", "640.102.150-03",
                new DateTime(1938, 7, 5), EnumeradorDeSexo.Feminino);

            var repositorioAluno = new RepositorioAluno();
            repositorioAluno.Add(aluno);

            repositorioAluno.Update(alunoAux);

            var alunoAtualizado = repositorioAluno.GetByContendoNoNome("Raimunda").First();

            Assert.True(alunoAtualizado.Equals(alunoAux) &&
                alunoAtualizado.Nome == alunoAux.Nome &&
                alunoAtualizado.CPF == alunoAux.CPF &&
                alunoAtualizado.Sexo == alunoAux.Sexo &&
                alunoAtualizado.Nascimento == alunoAux.Nascimento);
        }

        [Fact(DisplayName = "Atualizar aluno inexistente no repositório")]
        public void Atualizar_Aluno_Inexistente_No_Repositorio()
        {
            var aluno = new Aluno(201800774, "Nathan Lacerda", "48975163075",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino);

            var alunoAux = new Aluno(201800773, "Raimunda Maria", "48975163075",
                new DateTime(1938, 7, 5), EnumeradorDeSexo.Masculino);

            var repositorioAluno = new RepositorioAluno();
            repositorioAluno.Add(aluno);

            var excecao = Assert.Throws<Exception>(() => repositorioAluno.Update(alunoAux));
            Assert.Equal("Aluno não encontrado!", excecao.Message);
        }

        [Fact(DisplayName = "Atualizar aluno com CPF existente no repositório")]
        public void Atualizar_Aluno_Com_Cpf_Existente_No_Repositorio()
        {
            var aluno = new Aluno(201800774, "Nathan Lacerda", "48975163075",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino);

            var alunoAux = new Aluno(201800773, "Raimunda Maria", "640.102.150-03",
                new DateTime(1938, 7, 5), EnumeradorDeSexo.Masculino);

            var repositorioAluno = new RepositorioAluno();
            repositorioAluno.Add(aluno);

            repositorioAluno.Add(alunoAux);
            alunoAux.CPF = "48975163075";

            var excecao = Assert.Throws<Exception>(() => repositorioAluno.Update(alunoAux));
            Assert.Equal("CPF já registrado!", excecao.Message);
        }

        /*
         * FIM TESTE ATUALIZAÇÃO
         *

         *
         * INÍCIO TESTE MÉTODOS
         * UTILIZANDO aluno2 e aluno3
         */

        [Fact(DisplayName = "Retornar todos os alunos do repositório")]
        public void Retornar_Todos_Os_Alunos_Do_Repositorio()
        {
            var aluno = new Aluno(201800774, "Nathan Lacerda", "48975163075",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino);

            var alunoAux = new Aluno(201800773, "Raimunda Maria", "640.102.150-03",
                new DateTime(1938, 7, 5), EnumeradorDeSexo.Masculino);

            var repositorioAluno = new RepositorioAluno();
            repositorioAluno.Add(aluno);
            repositorioAluno.Add(alunoAux);

            var repositorioAlunoAux = new RepositorioAluno();
            repositorioAlunoAux.Add(aluno);
            repositorioAlunoAux.Add(alunoAux);

            Assert.Contains(repositorioAluno.GetAll().ToString(), repositorioAlunoAux.GetAll().ToString());
        }

        [Fact(DisplayName = "Retornar todos os alunos do repositório vazio")]
        public void Retornar_Todos_Os_Alunos_Do_Repositorio_Vazio()
        {
            var repositorioVazio = new RepositorioAluno();
            var excecao = Assert.Throws<Exception>(() => repositorioVazio.GetAll());
            Assert.Equal("Não existe nenhum aluno no repositório!", excecao.Message);
        }

        [Fact(DisplayName = "Retornar aluno do repositório")]
        public void Retornar_Aluno_Do_Repositorio()
        {
            var aluno = new Aluno(201800774, "Nathan Lacerda", "48975163075",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino);

            var alunoAux = new Aluno(201800773, "Raimunda Maria", "640.102.150-03",
                new DateTime(1938, 7, 5), EnumeradorDeSexo.Masculino);

            var repositorioAluno = new RepositorioAluno();
            repositorioAluno.Add(aluno);
            repositorioAluno.Add(alunoAux);

            var alunos = repositorioAluno.Get(alunoDoRepositorio => 
                             alunoDoRepositorio.Matricula == 201800774 || alunoDoRepositorio.Nome.Contains("Maria"));

            Assert.True(alunos.Contains(aluno) && alunos.Contains(alunoAux));
        }

        [Fact(DisplayName = "Pegar aluno por matrícula do repositório")]
        public void Pega_Aluno_Por_Matricula_Do_Repositorio()
        {
            var aluno = new Aluno(201800774, "Nathan Lacerda", "48975163075",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino);

            var repositorioAluno = new RepositorioAluno();
            repositorioAluno.Add(aluno);

            Assert.Equal(repositorioAluno.GetByMatricula(201800774), aluno);
        }

        [Fact(DisplayName = "Pegar aluno por matrícula inexistente do repositório vazio")]
        public void Pega_Aluno_Por_Matricula_Inexistente_Do_Repositorio_Vazio()
        {
            var aluno = new Aluno(201800774, "Nathan Lacerda", "48975163075",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino);

            var repositorioAluno = new RepositorioAluno();
            repositorioAluno.Add(aluno);

            var excecao = Assert.Throws<Exception>(() => repositorioAluno.GetByMatricula(5551));
            Assert.Equal("Não existe nenhum aluno com essa matrícula!", excecao.Message);
        }

        [Fact(DisplayName = "Pegar aluno por parte do nome do repositório")]
        public void Pega_Aluno_Por_Parte_Nome_Do_Repositorio()
        {
            var aluno = new Aluno(201800774, "Nathan Lacerda Pereira da Silva Nunes", "48975163075",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino);

            var repositorioAluno = new RepositorioAluno();
            repositorioAluno.Add(aluno);
            repositorioAluno.Add(new Aluno(201800773, "Nathan Lacerda Pereira da Silva Nunes", "640.102.150-03",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino));
            repositorioAluno.Add(new Aluno(201800772, "Nathan Lacerda Pereira Nunes", "071.395.200-89",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino));

            var alunos = repositorioAluno.Get(alunoDoRepositorio => alunoDoRepositorio.Nome.Contains("Silva"));

            Assert.True(alunos.ToArray().Length == 2);
        }

        [Fact(DisplayName = "Pegar aluno inexistente por parte do nome do repositório")]
        public void Pega_Aluno_Inexistente_Por_Parte_Nome_Do_Repositorio()
        {
            var aluno = new Aluno(201800774, "Nathan Lacerda Pereira da Silva Nunes", "48975163075",
                   new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino);

            var repositorioAluno = new RepositorioAluno();
            repositorioAluno.Add(aluno);
            repositorioAluno.Add(new Aluno(201800773, "Nathan Lacerda Pereira da Silva Nunes", "640.102.150-03",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino));
            repositorioAluno.Add(new Aluno(201800772, "Nathan Lacerda Pereira Nunes", "071.395.200-89",
                new DateTime(1999, 7, 5), EnumeradorDeSexo.Masculino));

            var alunos = repositorioAluno.Get(alunoDoRepositorio => alunoDoRepositorio.Nome.Contains("Silva"));

            var excecao = Assert.Throws<Exception>(() => repositorioAluno.GetByContendoNoNome("José"));
            Assert.Equal("Não existe nenhum aluno com esse nome!", excecao.Message);
        }
    }

    /*
     * FIM TESTE MÉTODOS
     */
}
