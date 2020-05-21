using EM.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EM.Repository
{
    public class RepositorioAluno : RepositorioAbstrato<Aluno>
    {
        /*private Object[]

        public RepositorioAluno()
        {
            Predicate<Aluno> predicate;
        }*/

        public override void Add(Aluno objeto)
        {
            throw new NotImplementedException();
        }

        public override void Remove(Aluno objeto)
        {
            throw new NotImplementedException();
        }

        public override void Update(Aluno objeto)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Aluno> GetAll()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Aluno> Get(Expression<Func<Aluno, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void GetByMatricula(int matricula)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Aluno> GetByContendoNoNome(string parteDonome)
        {
            throw new NotImplementedException();
        }
    }
}
