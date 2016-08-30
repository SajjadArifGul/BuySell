using BuySell.Contracts.Repositories;
using BuySell.DAL.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.DAL.Repository
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        internal DataContext context;
        internal DbSet<TEntity> dbSet;

        public RepositoryBase(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual TEntity GetByID(object ID)
        {
            return dbSet.Find(ID);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return dbSet;
        }
        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
                dbSet.Attach(entity);

            dbSet.Remove(entity);
            context.Entry(entity).State = EntityState.Deleted;
        }
        public virtual void Delete(object ID)
        {
            TEntity entity = dbSet.Find(ID);
            Delete(entity);
        }


        public virtual void Commit()
        {
            context.SaveChanges();
        }

        public virtual void Dispose()
        {
            context.Dispose();
        }
    }
}
