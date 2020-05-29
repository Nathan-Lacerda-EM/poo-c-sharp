using EM.Domain;
using static EM.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EM.Repository
{
    public class RepositorioAluno : RepositorioAbstrato<Aluno>
    {
        private List<Aluno> repositorioAlunos;

        public RepositorioAluno()
        {
            repositorioAlunos = new List<Aluno>();
        }

        public override void Add(Aluno aluno)
        {
            var alunos =
                from outroAluno in repositorioAlunos
                where outroAluno.Equals(aluno) || (aluno.CPF == outroAluno.CPF && aluno.CPF != "Sem CPF." &&
                    outroAluno.CPF != "Sem CPF.")
                select outroAluno;

            if (alunos.Count() > 0)
                throw new Exception("Aluno ou CPF já registrado!");

            repositorioAlunos.Add(aluno);
        }

        public override void Remove(Aluno aluno)
        {
            var alunos =
                from outroAluno in repositorioAlunos
                where outroAluno.Equals(aluno)
                select outroAluno;

            if (alunos.Count() == 0)
                throw new Exception("Aluno não encontrado!");

            repositorioAlunos.Remove(alunos.Single());
        }

        public override void Update(Aluno aluno)
        {
            var alunos =
                from alunoAtualizado in repositorioAlunos
                where alunoAtualizado.Equals(aluno)
                select alunoAtualizado;

            if (alunos.Count() == 0)
                throw new Exception("Aluno não encontrado!");

            var alunosCPF =
                from a in repositorioAlunos
                where !a.Equals(aluno) && (aluno.CPF == a.CPF && aluno.CPF != "Sem CPF." && a.CPF != "Sem CPF.")
                select a;

            if (alunosCPF.Count() > 0)
                throw new Exception("CPF já registrado!");

            repositorioAlunos.Remove(alunos.First());
            repositorioAlunos.Add(aluno);
        }

        public override IEnumerable<Aluno> GetAll()
        {
            var alunos =
                from aluno in repositorioAlunos
                orderby aluno.Matricula
                select aluno;

            if (alunos.Count() > 0)
                return alunos;
            else
                throw new Exception("Não existe nenhum aluno no repositório!");
        }

        public override IEnumerable<Aluno> Get(Expression<Func<Aluno, bool>> predicate)
        {
            Func<Aluno, bool> method = predicate.Compile();
            var alunos =
                from aluno in repositorioAlunos
                where method.Invoke(aluno)
                select aluno;

            if (alunos.Count() > 0)
                return alunos;
            else
                throw new Exception("Esse aluno não existe!");
        }

        public Aluno GetByMatricula(int matricula)
        {
            var alunos =
                from aluno in repositorioAlunos
                where aluno.Matricula == matricula
                select aluno;

            if (alunos.Count() > 0)
                return alunos.First();
            else
                throw new Exception("Não existe nenhum aluno com essa matrícula!");
        }

        public IEnumerable<Aluno> GetByContendoNoNome(string parteDoNome)
        {
            var alunos =
                from aluno in repositorioAlunos
                where RemoverAcentosEUppercase(aluno.Nome).Contains(RemoverAcentosEUppercase(parteDoNome))
                select aluno;

            if (alunos.Count() > 0)
                return alunos;
            else
                throw new Exception("Não existe nenhum aluno com esse nome!");
        }
    }
}
