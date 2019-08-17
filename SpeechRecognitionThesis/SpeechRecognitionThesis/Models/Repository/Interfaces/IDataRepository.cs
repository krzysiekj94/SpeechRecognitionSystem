using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.Repository
{
    public interface IDataRepository<TEntity>
    {
        IQueryable<TEntity> FindAll();
        EntityEntry Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
