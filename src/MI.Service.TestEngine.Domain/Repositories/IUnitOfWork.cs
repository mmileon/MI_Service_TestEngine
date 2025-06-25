using MI.Service.TestEngine.Domain.Entities;

namespace MI.Service.TestEngine.Domain.Repositories;

/// <summary>
/// Application unit of work.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Gets the application repository.
    /// </summary>
    /// <value>The application repository.</value>
    IRepository<Application> ApplicationRepository { get; }

    /// <summary>
    /// Saves the changes asynchronous.
    /// </summary>
    Task<int> SaveChangesAsync();
}
