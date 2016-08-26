using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Contracts.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        void Commit();
        void Delete(TEntity entity);
        void Delete(object ID);
        void Dispose();
        IQueryable<TEntity> GetAll();
        TEntity GetByID(object ID);
        void Insert(TEntity entity);
        void Update(TEntity entity);
    }
}
