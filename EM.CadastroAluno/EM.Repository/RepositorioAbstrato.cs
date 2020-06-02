using EM.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EM.Repository
{
    public abstract class RepositorioAbstrato<T> where T : IEntidade
    {
        protected readonly List<T> repositorio = new List<T>();
        public abstract void Add(T objeto);
        public abstract void Remove(T objeto);
        public abstract void Update(T objeto);
        public abstract IEnumerable<T> GetAll();
        public abstract IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
    }
}
