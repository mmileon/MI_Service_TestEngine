namespace MI.Service.TestEngine.Infrastructure.Persistence.Repositories;

/// <inheritdoc/>
public class Repository<TEntity> : BaseRepository<ApplicationDbContext, TEntity> where TEntity : class
{
    /// <summary>
    /// Initializes a new instance of the Repository class.
    /// </summary>
    /// <param name="context">The context.</param>
    public Repository(ApplicationDbContext context)
        : base(context)
    { }
}
