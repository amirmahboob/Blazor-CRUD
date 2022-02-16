using Mc2.CrudTest.Presentation.Domain;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Presentation.Data
{
    public class PresentationContext : DbContext, IUnitOfWork
    {
        public PresentationContext()
        {
        }

        public PresentationContext(DbContextOptions<PresentationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public override int SaveChanges()
        {
            return SaveAllChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public void MyChangeTracker(EntityState state)
        {
            var temp = ChangeTracker.Entries().ToList();
            foreach (var item in temp)
            {
                item.State = state;
            }
        }
        public int SaveAllChanges()
        {
            var result = base.SaveChanges();
            return result;
        }
        public Task<int> SaveAllChangesAsync(bool invalidateCacheDependencies = true)
        {
            var result = base.SaveChangesAsync();
            return result;
        }
    }
}