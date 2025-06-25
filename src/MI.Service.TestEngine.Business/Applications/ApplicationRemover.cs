using MI.Service.TestEngine.Domain.Repositories;
using MI.Service.TestEngine.Shared;
using MI.Service.TestEngine.Shared.Exceptions;

namespace MI.Service.TestEngine.Business.Applications;

/// <summary>
/// Provides services for application remove.
/// </summary>
public sealed class ApplicationRemover : BaseService
{
    private readonly IUnitOfWork unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationRemover"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="serviceProvider">The service provider.</param>
    public ApplicationRemover(
        IServiceProvider serviceProvider,
        IUnitOfWork unitOfWork
    ) : base(serviceProvider)
    {
        this.unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Deletes the application asynchronous.
    /// </summary>
    /// <param name="name">The application name.</param>
    public async Task DeleteAsync(string name)
    {
        var application = await this.unitOfWork.ApplicationRepository.FindOneAsync(x => x.Name == name);

        if (application == null)
            throw new DataNotFoundException(ErrorCodes.ApplicationNotFound, "Application not found.");

        if (await this.unitOfWork.RuleRepository.ExistsAsync(x => x.ApplicationSystemName == application.SystemName))
            throw new InvalidBusinessException(ErrorCodes.RuleExistForApplication, "Rule exist for this application.");

        if (await this.unitOfWork.TestRepository.ExistsAsync(x => x.ApplicationSystemName == application.SystemName))
            throw new InvalidBusinessException(ErrorCodes.TestExistForApplication, "Test exist for this application.");

        await this.unitOfWork.ModuleRepository.RemoveAsync(x => x.ApplicationSystemName == application.SystemName);
        await this.unitOfWork.ApplicationRepository.RemoveAsync(x => x.SystemName == application.SystemName);

        await this.unitOfWork.SaveChangesAsync();
    }
}