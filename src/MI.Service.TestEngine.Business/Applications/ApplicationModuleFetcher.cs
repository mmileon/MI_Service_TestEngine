using MI.Service.TestEngine.Business.Applications.Models;
using MI.Service.TestEngine.Domain.Repositories;

namespace MI.Service.TestEngine.Business.Applications;

/// <summary>
/// Provides services for application and module fetchers.
/// </summary>
public sealed class ApplicationModuleFetcher : BaseService
{
    private readonly IUnitOfWork unitOfWork;

    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <param name="unitOfWork">The unit of work.</param>
    public ApplicationModuleFetcher(
        IServiceProvider serviceProvider,
        IUnitOfWork unitOfWork
    ) : base(serviceProvider)
    {
        this.unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Get all application modules options.
    /// </summary>
    public async Task<List<ApplicationModuleModel>> GetsAsync()
    {
        var applicationModules = await this.unitOfWork.SharedRepository.GetApplicationModuleAsync();

        return applicationModules.GroupBy(x => x.ApplicationName)
            .Select(y => new ApplicationModuleModel
            {
                Application = y.Key,
                Modules = y.Select(x => x.ModuleName).ToList()
            }).ToList();
    }
}
