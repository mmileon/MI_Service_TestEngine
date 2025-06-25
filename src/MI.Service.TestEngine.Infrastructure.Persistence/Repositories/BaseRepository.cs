using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MI.Service.TestEngine.Domain.Repositories;

namespace MI.Service.TestEngine.Infrastructure.Persistence.Repositories;

/// <summary>
/// Base repository.
/// </summary>
/// <typeparam name="TDbContext"></typeparam>
/// <typeparam name="TEntity">Repository entity.</typeparam>
public abstract class BaseRepository<TDbContext, TEntity> : IRepository<TEntity>
    where TEntity : class
    where TDbContext : DbContext
{
    /// <summary>
    /// The context
    /// </summary>
    internal readonly TDbContext Context;

    /// <summary>
    /// The database set
    /// </summary>
    private readonly DbSet<TEntity> dbSet;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseRepository{TDbContext, TEntity}"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    protected BaseRepository(TDbContext context)
    {
        this.Context = context;
        this.dbSet = this.Context.Set<TEntity>();
    }

    /// <inheritdoc/>
    public virtual async Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await this.dbSet.Where(predicate).ToListAsync();
    }

    /// <inheritdoc/>
    public virtual IQueryable<TEntity> GetAll()
    {
        return this.dbSet;
    }

    /// <inheritdoc/>
    public virtual async Task<List<TEntity>> GetAllAsync()
    {
        return await this.dbSet.ToListAsync();
    }

    /// <inheritdoc/>
    public virtual async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await this.dbSet.FirstOrDefaultAsync(predicate);
    }

    /// <inheritdoc/>
    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await this.dbSet.AnyAsync(predicate);
    }

    /// <inheritdoc/>
    public virtual async Task AddAsync(TEntity entity)
    {
        await this.dbSet.AddAsync(entity);
    }

    /// <inheritdoc/>
    public virtual async Task AddAsync(IEnumerable<TEntity> entities)
    {
        await this.dbSet.AddRangeAsync(entities);
    }

    /// <summary>
    /// Updates the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    public virtual async Task UpdateAsync(TEntity entity)
    {
        await Task.Run(() => this.dbSet.Update(entity));
    }

    /// <summary>
    /// Updates the specified entities.
    /// </summary>
    /// <param name="entities">The entities.</param>
    public virtual async Task UpdateAsync(List<TEntity> entities)
    {
        await Task.Run(() => this.dbSet.UpdateRange(entities));
    }

    /// <inheritdoc/>
    public virtual async Task RemoveAsync(TEntity entity)
    {
        await Task.Run(() => this.dbSet.Remove(entity));
    }

    /// <inheritdoc/>
    public virtual async Task RemoveAsync(List<TEntity> entities)
    {
        await Task.Run(() => this.dbSet.RemoveRange(entities));
    }

    /// <inheritdoc/>
    public virtual async Task RemoveAsync(Expression<Func<TEntity, bool>> predicate)
    {
        await Task.Run(() =>
        {
            var objects = this.dbSet.Where(predicate);
            this.dbSet.RemoveRange(objects);
        });
    }
}
