using Microsoft.Extensions.DependencyInjection;
using MI.Service.TestEngine.Domain.Entities;
using MI.Service.TestEngine.Domain.Repositories;

namespace MI.Service.TestEngine.Infrastructure.Persistence;

/// <summary>
/// Database unit of work
/// </summary>
public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext context;

    private readonly IServiceProvider serviceProvider;

    
    /// <summary>
    /// Repository for rule application.
    /// </summary>
    public IRepository<Application> ApplicationRepository => this.serviceProvider.GetService<IRepository<Application>>();

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <param name="context">The context.</param>
    public UnitOfWork(
        IServiceProvider serviceProvider,
        ApplicationDbContext context
    )
    {
        this.serviceProvider = serviceProvider;
        this.context = context;
    }

    /// <summary>
    /// Saves the changes asynchronous.
    /// </summary>
    public async Task<int> SaveChangesAsync()
    {
        return await this.context.SaveEntitiesAsync();
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting
    /// unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        this.context?.Dispose();
    }
}
