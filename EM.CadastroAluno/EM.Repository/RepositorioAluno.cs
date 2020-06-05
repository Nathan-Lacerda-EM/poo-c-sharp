using EM.Domain;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static EM.Domain.Utils;

namespace EM.Repository
{
    public class RepositorioAluno : RepositorioAbstrato<Aluno>
    {
        GerenciadorBancoDeDados gerenciadorBancoDeDados = GerenciadorBancoDeDados.GetInstancia;

        public override void Add(Aluno aluno)
        {
            var colecaoDeAlunos = Get(alunoDoRepositorio =>
                alunoDoRepositorio.Equals(aluno) ||
                (aluno.CPF == alunoDoRepositorio.CPF &&
                aluno.CPF != "Sem CPF." &&
                alunoDoRepositorio.CPF != "Sem CPF."));

            if (colecaoDeAlunos.Count() > 0)
            {
                throw new Exception("Aluno ou CPF já registrado!");
            }

            if (!InserirAluno(aluno))
            {
                throw new Exception("Não foi possível inserir o aluno do banco de dados.");
            }
        }

        public override void Remove(Aluno aluno)
        {
            var colecaoDeAlunos = Get(alunoDoRepositorio => alunoDoRepositorio.Equals(aluno));

            if (colecaoDeAlunos.Count() == 0)
            {
                throw new Exception("Aluno não encontrado!");
            }

            if (!RemoverAluno(colecaoDeAlunos.First()))
            {
                throw new Exception("Não foi possível remover o aluno do banco de dados.");
            }
        }

        public override void Update(Aluno aluno)
        {
            var colecaoDeAlunos = Get(alunoDoRepositorio => alunoDoRepositorio.Equals(aluno));

            if (colecaoDeAlunos.Count() == 0)
            {
                throw new Exception("Aluno não encontrado!");
            }

            var colecaoDeAlunosCPF = Get(alunoDoRepositorio =>
                !alunoDoRepositorio.Equals(aluno) &&
                (aluno.CPF == alunoDoRepositorio.CPF &&
                aluno.CPF != "Sem CPF." &&
                alunoDoRepositorio.CPF != "Sem CPF."));

            if (colecaoDeAlunosCPF.Count() > 0)
            {
                throw new Exception("CPF já registrado!");
            }

            if (!AtualizarAluno(aluno, colecaoDeAlunos.First()))
            {
                throw new Exception("Não foi possível atualizar o aluno no banco de dados.");
            }
        }

        public override IEnumerable<Aluno> GetAll()
        {
            AtualizeListaDeAlunos();

            var colecaoDeAlunos =
                from aluno in repositorio
                orderby aluno.Matricula
                select aluno;

            if (colecaoDeAlunos.Count() == 0)
            {
                throw new Exception("Não existe nenhum aluno no repositório!");
            }

            return colecaoDeAlunos;
        }

        public override IEnumerable<Aluno> Get(Expression<Func<Aluno, bool>> predicate)
        {
            AtualizeListaDeAlunos();

            Func<Aluno, bool> expressao = predicate.Compile();

            return from aluno in repositorio
                   where expressao.Invoke(aluno)
                   select aluno;
        }

        public Aluno GetByMatricula(int matricula)
        {
            var colecaoDeAlunos = Get(alunoDoRepositorio => alunoDoRepositorio.Matricula == matricula);

            if (colecaoDeAlunos.Count() == 0)
            {
                throw new Exception("Não existe nenhum aluno com essa matrícula!");
            }

            return colecaoDeAlunos.First();
        }

        public IEnumerable<Aluno> GetByContendoNoNome(string parteDoNome)
        {
            var colecaoDeAlunos = Get(alunoDoRepositorio =>
                RemovaAcentosEUppercase(alunoDoRepositorio.Nome).Contains(RemovaAcentosEUppercase(parteDoNome)));

            if (colecaoDeAlunos.Count() == 0)
            {
                throw new Exception("Não existe nenhum aluno com esse nome!");
            }

            return colecaoDeAlunos;
        }

        private bool InserirAluno(Aluno aluno)
        {
            try
            {
                string comando = "INSERT INTO TBALUNO(MATRIC, NOME, CPF, NASC, SEXO) VALUES (@MATRIC, @NOME, @CPF, @NASC, @SEXO);";

                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("MATRIC", aluno.Matricula);
                parametros.Add("NOME", aluno.Nome);
                parametros.Add("CPF", aluno.CPF);
                parametros.Add("NASC", aluno.Nascimento);
                parametros.Add("SEXO", aluno.Sexo);

                GerenciadorBancoDeDados.GetInstancia.ExecuteComandoComParametro(comando, parametros);

                repositorio.Add(aluno);
                return true;
            }
            catch (FbException fbException)
            {
                return false;
            }
        }

        private bool RemoverAluno(Aluno aluno)
        {
            try
            {
                string comando = "DELETE FROM TBALUNO WHERE MATRIC = @MATRIC;";
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("MATRIC", aluno.Matricula);

                gerenciadorBancoDeDados.ExecuteComandoComParametro(comando, parametros);

                repositorio.Remove(aluno);
                return true;
            }
            catch (FbException fbException)
            {
                return false;
            }
        }

        private bool AtualizarAluno(Aluno alunoAtualizado, Aluno alunoAntigo)
        {
            try
            {
                string comando = "UPDATE TBALUNO SET NOME = @NOME, CPF = @CPF, NASC = @NASC, SEXO = @SEXO" +
                    " WHERE MATRIC = @MATRIC;";
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("MATRIC", alunoAtualizado.Matricula);
                parametros.Add("NOME", alunoAtualizado.Nome);
                parametros.Add("CPF", alunoAtualizado.CPF);
                parametros.Add("NASC", alunoAtualizado.Nascimento);
                parametros.Add("SEXO", alunoAtualizado.Sexo);

                GerenciadorBancoDeDados.GetInstancia.ExecuteComandoComParametro(comando, parametros);

                repositorio.Remove(alunoAntigo);
                repositorio.Add(alunoAtualizado);
                return true;
            }
            catch (FbException)
            {
                return false;
            }
        }

        private bool AtualizeListaDeAlunos()
        {
            try
            {
                var comando = "SELECT * FROM TBALUNO;";
                var fbDataReader = GerenciadorBancoDeDados.GetInstancia.ExecuteComandoRetornandoReader(comando);
                var colecaoDeAlunos = new List<Aluno>();

                if (!fbDataReader.HasRows)
                {
                    repositorio = colecaoDeAlunos;
                    gerenciadorBancoDeDados.GetConexao.Close();
                    return true;
                }

                while (fbDataReader.Read())
                {
                    var aluno = new Aluno
                    {
                        Matricula = fbDataReader.GetInt32(0),
                        Nome = fbDataReader.GetString(1),
                        CPF = LimpeCPF(fbDataReader.GetString(2)),
                        Nascimento = fbDataReader.GetDateTime(3),
                        Sexo = (EnumeradorDeSexo)fbDataReader.GetInt32(4)
                    };
                    colecaoDeAlunos.Add(aluno);
                }

                repositorio = colecaoDeAlunos;
                gerenciadorBancoDeDados.GetConexao.Close();
                return true;
            }
            catch (FbException)
            {
                return false;
            }
        }
    }
}
