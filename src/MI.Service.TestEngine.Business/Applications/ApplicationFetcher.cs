using AutoMapper;
using MI.Service.TestEngine.Business.Models;
using MI.Service.TestEngine.Domain.Repositories;

namespace MI.Service.TestEngine.Business.Applications;

/// <summary>
/// Provides services for get application data.
/// </summary>
public sealed class ApplicationFetcher : BaseService
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;

    /// <summary>Initializes a new instance of the <see cref="ApplicationService" /> class.</summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="unitOfWork">The unit of work.</param>
    public ApplicationFetcher(
        IServiceProvider serviceProvider,
        IMapper mapper,
        IUnitOfWork unitOfWork
    ) : base(serviceProvider)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;        
    }

    /// <summary>
    /// Get applications options.
    /// </summary>
    public async Task<List<NameModel>> GetsAsync()
    {        
        var applications = await this.unitOfWork.ApplicationRepository.GetAllAsync();
        return this.mapper.Map<List<NameModel>>(applications);
    }

    /// <summary>
    /// Gets all application asynchronous.
    /// </summary>
    public async Task<IReadOnlyCollection<ExternalReferenceModel>> GetsExternalAsync()
    {        
        var applications = await this.unitOfWork.ApplicationRepository.GetAllAsync();     
        return this.mapper.Map<IReadOnlyCollection<ExternalReferenceModel>>(applications);
    }
}
