using System.Linq.Expressions;

namespace MI.Service.TestEngine.Domain.Repositories;

/// <summary>
/// Provide the declaration of base repository methods.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public interface IRepository<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Wheres the asynchronous.
    /// </summary>
    /// <param name="predicate">The predicate.</param>
    /// <returns></returns>
    Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Gets all as querable.
    /// </summary>
    /// <returns></returns>
    IQueryable<TEntity> GetAll();

    /// <summary>
    /// Gets all asynchronous.
    /// </summary>
    /// <returns></returns>
    Task<List<TEntity>> GetAllAsync();

    /// <summary>
    /// Finds the one asynchronous.
    /// </summary>
    /// <param name="predicate">The predicate.</param>
    /// <returns></returns>
    Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Exists the specified predicate asynchronous.
    /// </summary>
    /// <param name="predicate">The predicate.</param>
    /// <returns></returns>
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Adds the asynchronous.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns></returns>
    Task AddAsync(TEntity entity);

    /// <summary>
    /// Adds the asynchronous.
    /// </summary>
    /// <param name="entities">The entities.</param>
    /// <returns></returns>
    Task AddAsync(IEnumerable<TEntity> entities);

    /// <summary>
    /// Updates the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    Task UpdateAsync(TEntity entity);

    /// <summary>
    /// Updates the specified entities.
    /// </summary>
    /// <param name="entities">The entities.</param>
    Task UpdateAsync(List<TEntity> entities);

    /// <summary>
    /// Removes the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    Task RemoveAsync(TEntity entity);

    /// <summary>
    /// Removes the specified entities.
    /// </summary>
    /// <param name="entities">The entities.</param>
    Task RemoveAsync(List<TEntity> entities);

    /// <summary>
    /// Removes the specified predicate.
    /// </summary>
    /// <param name="predicate">The predicate.</param>
    Task RemoveAsync(Expression<Func<TEntity, bool>> predicate);
}
