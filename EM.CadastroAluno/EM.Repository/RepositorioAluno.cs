using EM.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static EM.Domain.Utils;

namespace EM.Repository
{
    public class RepositorioAluno : RepositorioAbstrato<Aluno>
    {
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

            repositorio.Add(aluno);
        }

        public override void Remove(Aluno aluno)
        {
            var colecaoDeAlunos = Get(alunoDoRepositorio => alunoDoRepositorio.Equals(aluno));

            if (colecaoDeAlunos.Count() == 0)
            {
                throw new Exception("Aluno não encontrado!");
            }

            repositorio.Remove(colecaoDeAlunos.First());
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

            repositorio.Remove(colecaoDeAlunos.First());
            repositorio.Add(aluno);
        }

        public override IEnumerable<Aluno> GetAll()
        {
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
    }
}
