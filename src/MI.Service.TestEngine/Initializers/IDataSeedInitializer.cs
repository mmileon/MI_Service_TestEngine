namespace MI.Service.TestEngine.Initializers;

/// <summary>
/// Database initializer for statistics service.
/// </summary>
public interface IDataSeedInitializer
{
    /// <summary>
    /// Initializes the seed data.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <returns></returns>
    Task InitializeSeedData(IServiceProvider serviceProvider);

    /// <summary>Imports the seed data.</summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <param name="accountId">The account identifier.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    Task ImportSeedData(IServiceProvider serviceProvider, string accountId);
}
