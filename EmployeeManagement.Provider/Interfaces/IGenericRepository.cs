using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagement.Data.Interfaces
{
    /// <summary>
    /// Generic Repository for Data Access.
    /// </summary>
    /// <typeparam name="TEntity">Entity.</typeparam>
    public interface IGenericRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Get all values.
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns>Enumerable of Entity Type.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Get dbset.
        /// </summary>
        /// <returns>dbset.</returns>
        DbSet<TEntity> GetContext();

        /// <summary>
        /// Get a value by predicate.
        /// </summary>
        /// <param name="predicate">predicate expression to find value.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns>Value of entity.</returns>
        Task<TEntity> FindByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Find specific values.
        /// </summary>
        /// <param name="predicate">Predicate expression to find values.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns>Enumerable of Entity Type.</returns>
        Task<IEnumerable<TEntity>> FindAllByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Find specific values.
        /// </summary>
        /// <param name="predicate">Predicate expression to find values.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns>Enumerable of Entity Type.</returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Insert a value.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="cancellationToken">ancellation Token.</param>
        /// <returns>Task.</returns>
        ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity, CancellationToken cancellationToken);

        /// <summary>
        /// Insert range of value.
        /// </summary>
        /// <param name="entities">Entities.</param>
        /// <param name="cancellationToken">ancellation Token.</param>
        /// <returns>Task.</returns>
        Task InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

        /// <summary>
        /// Delete all values based on predicate.
        /// </summary>
        /// <param name="predicate">Predicate expression to find values.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns>Task.</returns>
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Delete specific entity by entity.
        /// </summary>
        /// <param name="entityToDelete">Entity.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns>Task.</returns>
        Task DeleteAsync(TEntity entityToDelete, CancellationToken cancellationToken);

        /// <summary>
        /// Update specific entity by entity.
        /// </summary>
        /// <param name="entityToUpdate">Entity.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns>Task.</returns>
        Task UpdateAsync(TEntity entityToUpdate, CancellationToken cancellationToken);

    }
}
