using EmployeeManagement.Data.EF;
using EmployeeManagement.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagement.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
         where TEntity : class
    {
        private readonly DbSet<TEntity> dbSet;
        private readonly EmployeeContext employeeContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="employeeContext">EmployeeContext DbContext.</param>
        public GenericRepository(EmployeeContext employeeContext)
        {
            this.employeeContext = employeeContext;

            if (employeeContext != null)
            {
                this.employeeContext.Database.SetCommandTimeout(TimeSpan.FromMinutes(3));
                dbSet = this.employeeContext.Set<TEntity>();
            }
        }

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includes)
        {

            return await includes.Aggregate(dbSet.AsQueryable(), (query, path) => query.Include(path)).AsNoTracking().ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual DbSet<TEntity> GetContext()
        {
            return this.dbSet;
        }

        /// <inheritdoc/>
        public virtual Task<TEntity> FindByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includes)
        {
            foreach (Expression<Func<TEntity, object>> include in includes)
                dbSet.Include(include);
            return dbSet
                    .Where(predicate)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(cancellationToken)
;
        }

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<TEntity>> FindAllByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includes)
        {
            dbSet.AsNoTracking();
            foreach (Expression<Func<TEntity, object>> include in includes)
                dbSet.Include(include);
            return await dbSet
                    .Where(predicate)
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(true);
        }

        /// <inheritdoc/>
        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includes)
        {
            foreach (Expression<Func<TEntity, object>> include in includes)
                dbSet.Include(include);
            return dbSet.AnyAsync(predicate, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity, CancellationToken cancellationToken)
        {
            return dbSet.AddAsync(entity, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual Task InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            return dbSet.AddRangeAsync(entities, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            var entityToDelete = await FindByAsync(predicate, cancellationToken).ConfigureAwait(true);
            await DeleteAsync(entityToDelete, cancellationToken).ConfigureAwait(true);
        }
        /// <inheritdoc/>
        public virtual Task DeleteAsync(TEntity entityToDelete, CancellationToken cancellationToken)
        {
            return Task.Run(
                () =>
                {
                    if (employeeContext.Entry(entityToDelete).State == EntityState.Detached)
                    {
                        dbSet.Remove(entityToDelete);
                    }

                    dbSet.Remove(entityToDelete);
                }, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual Task UpdateAsync(TEntity entityToUpdate, CancellationToken cancellationToken)
        {
            return Task.Run(
                () =>
                {
                    dbSet.Update(entityToUpdate);
                }, cancellationToken);
        }

       
       
    }

}
