﻿using Mc2.CrudTest.Presentation.Domain;
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
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.FirstName).IsRequired();
                entity.Property(e => e.LastName).IsRequired();
                entity.Property(e => e.DateOfBirth).IsRequired();
                entity.Property(e => e.PhoneNumber).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.BankAccountNumber).IsRequired();
                //entity.Property(e => new { e.FirstName, e.LastName, e.DateOfBirth, e.PhoneNumber, e.Email, e.BankAccountNumber }).IsRequired();
                entity.HasIndex(e => new { e.FirstName, e.LastName, e.DateOfBirth }).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
            });

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