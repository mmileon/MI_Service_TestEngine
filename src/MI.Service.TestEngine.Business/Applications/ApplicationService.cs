using MI.Service.TestEngine.Business.Applications.Models;
using MI.Service.TestEngine.Business.Models;

namespace MI.Service.TestEngine.Business.Applications;

/// <summary>
/// Provides services for application.
/// </summary>
public sealed class ApplicationService
{
    private readonly ApplicationFetcher applicationFetcher;
    private readonly ApplicationModuleFetcher applicationModuleFetcher;
    private readonly ApplicationModuleUpsert applicationModuleUpsert;
    private readonly ApplicationUpsert applicationUpsert;
    private readonly ApplicationRemover applicationRemover;

    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    /// <param name="applicationFetcher">The application fetcher.</param>
    /// <param name="moduleFetcher">The module fetcher.</param>
    /// <param name="applicationModuleFetcher">The application module fetcher.</param>
    /// <param name="applicationModuleUpsert">The application module upsert.</param>
    /// <param name="applicationUpsert">The application upsert.</param>
    /// <param name="moduleUpsert">The module upsert.</param>
    /// <param name="applicationRemover">The application remover.</param>
    /// <param name="moduleRemover">The module remover.</param>
    public ApplicationService(
        ApplicationFetcher applicationFetcher
        , ApplicationModuleFetcher applicationModuleFetcher
        , ApplicationModuleUpsert applicationModuleUpsert
        , ApplicationUpsert applicationUpsert
        , ApplicationRemover applicationRemover
       )
    {
        this.applicationFetcher = applicationFetcher;
        this.applicationModuleFetcher = applicationModuleFetcher;
        this.applicationModuleUpsert = applicationModuleUpsert;
        this.applicationUpsert = applicationUpsert;
        this.applicationRemover = applicationRemover;
    }

    /// <summary>
    /// Get applications options.
    /// </summary>
    public async Task<List<NameModel>> GetApplicationsAsync() =>
        await applicationFetcher.GetsAsync();

    /// <summary>
    /// Gets all application asynchronous.
    /// </summary>
    /// <returns>The application list.</returns>
    public async Task<IReadOnlyCollection<ExternalReferenceModel>> GetAllAsync() =>
        await applicationFetcher.GetsExternalAsync();

    /// <summary>
    /// Get rule application modules options.
    /// </summary>
    public async Task<List<NameModel>> GetModulesAsync(string applicationName) =>
        await this.moduleFetcher.GetsAsync(applicationName);

    /// <summary>
    /// Get rule application modules options.
    /// </summary>
    public async Task<List<ApplicationModuleModel>> GetApplicationModulesAsync() =>
        await applicationModuleFetcher.GetsAsync();

    /// <summary>
    /// Creates rules application asynchronous.
    /// </summary>
    /// <param name="applicationModel">The model.</param>
    public async Task<NameModel> CreateApplicationModulesAsync(ApplicationModuleModel applicationModel) =>
        await applicationModuleUpsert.CreateAsync(applicationModel);

    /// <summary>
    /// Creates rules application asynchronous.
    /// </summary>
    /// <param name="model">The name model.</param>
    public async Task<NameModel> CreateApplicationAsync(NameModel model) =>
        await applicationUpsert.CreateAsync(model);

    /// <summary>
    /// Renames the application asynchronous.
    /// </summary>
    /// <param name="applicationName">Name of the application.</param>
    /// <param name="model">The model.</param>
    /// <returns></returns>
    public async Task<NameModel> RenameApplicationAsync(string applicationName, NameModel model) =>
        await applicationUpsert.RenameAsync(applicationName, model);

    /// <summary>
    /// Deletes the application asynchronous.
    /// </summary>
    /// <param name="name">The application name.</param>
    public async Task DeleteApplicationAsync(string name) =>
        await applicationRemover.DeleteAsync(name);
}
