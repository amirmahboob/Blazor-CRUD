using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Data
{
    public interface IUnitOfWork : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
        int SaveAllChanges();
        Task<int> SaveAllChangesAsync(bool invalidateCacheDependencies = true);
        void MyChangeTracker(EntityState state);
    }
}
