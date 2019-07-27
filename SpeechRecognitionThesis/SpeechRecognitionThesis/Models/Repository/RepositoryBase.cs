using Microsoft.EntityFrameworkCore;
using SpeechRecognitionThesis.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.Repository
{
    public abstract class RepositoryBase<T> : IDataRepository<T> where T : class
    {
        protected RepositoryContext RepositoryContext { get; set; }

        public RepositoryBase(RepositoryContext repositoryContext )
        {
            this.RepositoryContext = repositoryContext;
        }

        public void Add(T entity)
        {
            this.RepositoryContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            this.RepositoryContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return this.RepositoryContext.Set<T>().AsNoTracking();
        }

        public void Update(T entity)
        {
            this.RepositoryContext.Set<T>().Update(entity);
        }
    }
}
