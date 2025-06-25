using MI.Service.TestEngine.Business.Applications.Models;
using MI.Service.TestEngine.Business.Models;
using MI.Service.TestEngine.Domain.Entities;
using MI.Service.TestEngine.Domain.Repositories;
using MI.Service.TestEngine.Shared;
using MI.Service.TestEngine.Shared.Exceptions;

namespace MI.Service.TestEngine.Business.Applications;

/// <summary>
/// Provides services for application and module create and update.
/// </summary>
public sealed class ApplicationModuleUpsert : BaseService
{
    private readonly IUnitOfWork unitOfWork;

    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="serviceProvider">The service provider.</param>
    public ApplicationModuleUpsert(
        IServiceProvider serviceProvider,
        IUnitOfWork unitOfWork
    ) : base(serviceProvider)
    {
        this.unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Creates rules application asynchronous.
    /// </summary>
    /// <param name="applicationModel">The model.</param>
    public async Task<NameModel> CreateAsync(ApplicationModuleModel applicationModel)
    {
        var application = await this.unitOfWork.ApplicationRepository.FindOneAsync(x => x.Name == applicationModel.Application);

        if (application != null)
            throw new ConflictException(ErrorCodes.ApplicationConflict, "Application conflict");

        application = new Application
        {
            Name = applicationModel.Application,
            IsActive = true
        };

        await unitOfWork.ApplicationRepository.AddAsync(application);
        await unitOfWork.SaveChangesAsync();

        if (applicationModel.Modules != null)
        {
            var modules = applicationModel.Modules.Select(x => new Module
            {
                Name = x,
                IsActive = true,
                ApplicationSystemName = application.SystemName
            }).ToList();

            await unitOfWork.ModuleRepository.AddAsync(modules);
            await unitOfWork.SaveChangesAsync();
        }

        return new NameModel { Name = application.Name };
    }
}
