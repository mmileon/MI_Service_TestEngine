using MI.Service.TestEngine.Business.Models;
using MI.Service.TestEngine.Domain.Entities;
using MI.Service.TestEngine.Domain.Repositories;
using MI.Service.TestEngine.Shared;
using MI.Service.TestEngine.Shared.Exceptions;

namespace MI.Service.TestEngine.Business.Applications;

/// <summary>
/// Provides services for application create and update.
/// </summary>
public sealed class ApplicationUpsert : BaseService
{
    private readonly IUnitOfWork unitOfWork;

    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="serviceProvider">The service provider.</param>
    public ApplicationUpsert(
        IServiceProvider serviceProvider,
        IUnitOfWork unitOfWork
    ) : base(serviceProvider)
    {
        this.unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Creates rules application asynchronous.
    /// </summary>
    /// <param name="model">The name model.</param>
    public async Task<NameModel> CreateAsync(NameModel model)
    {
        var application = await this.unitOfWork.ApplicationRepository.FindOneAsync(x => x.Name == model.Name);

        if (application != null)
            throw new ConflictException(ErrorCodes.ApplicationConflict, "Application conflict");

        application = new Application
        {
            Name = model.Name,
            IsActive = true
        };

        await unitOfWork.ApplicationRepository.AddAsync(application);
        await unitOfWork.SaveChangesAsync();

        return new NameModel { Name = application.Name };
    }

    /// <summary>
    /// Renames the application asynchronous.
    /// </summary>
    /// <param name="applicationName">Name of the application.</param>
    /// <param name="model">The model.</param>
    public async Task<NameModel> RenameAsync(string applicationName, NameModel model)
    {
        var application = await this.unitOfWork.ApplicationRepository.FindOneAsync(x => x.Name == applicationName);

        if (application == null)
            throw new DataNotFoundException(ErrorCodes.ApplicationNotFound, "Application not found.");

        if (await this.unitOfWork.ApplicationRepository.ExistsAsync(x => x.Name.Equals(model.Name)))
            throw new ConflictException(ErrorCodes.ApplicationConflict, "Application conflict");

        application.Name = model.Name;

        await unitOfWork.ApplicationRepository.UpdateAsync(application);
        await unitOfWork.SaveChangesAsync();

        return new NameModel { Name = application.Name };
    }
}