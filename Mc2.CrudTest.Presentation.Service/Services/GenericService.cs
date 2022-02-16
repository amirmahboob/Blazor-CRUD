using Mc2.CrudTest.Presentation.Data;
using Mc2.CrudTest.Presentation.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Mc2.CrudTest.Presentation.Service.Services
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
    {
        protected IUnitOfWork Uow;
        protected DbSet<TEntity> Entities;

        public GenericService(IUnitOfWork uow)
        {
            Uow = uow;
            Entities = Uow.Set<TEntity>();
        }
        public virtual TEntity Add(TEntity entity)
        {
            return Entities.Add(entity).Entity;
        }

        public virtual void Update(TEntity entity)
        {
            Uow.MyChangeTracker(EntityState.Detached);
            Entities.Update(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            Entities.Remove(entity);
        }

        public virtual TEntity Find(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = Entities;
            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);
            return query.FirstOrDefault(predicate);
        }

        public virtual async Task<IList<TEntity>> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = Entities;
            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);
            if (orderBy != null)
                query = orderBy(query);
            return await query.ToListAsync();
        }

        public virtual async Task<IList<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = Entities;
            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);
            if (orderBy != null)
                query = orderBy(query);
            if (predicate != null)
                query = query.Where(predicate);
            return await query.ToListAsync();
        }
        #region IDisposable Members
        public void Dispose()
        {
            //throw new NotImplementedException();
        }
        #endregion
    }
}
