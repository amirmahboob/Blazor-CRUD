using System.Linq.Expressions;

namespace Mc2.CrudTest.Presentation.Service.Interface
{
    public interface IGenericService<T> : IDisposable where T : class
    {
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        T Find(Func<T, bool> predicate, params Expression<Func<T, object>>[] includes);
        Task<IList<T>> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] includes);
        Task<IList<T>> GetAll(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] includes);
    }
}
