namespace MI.Service.TestEngine.Initializers;

/// <summary>
/// Database entries initializer.
/// </summary>
public interface IDatabaseEntriesInitializer
{
    /// <summary>
    /// Initializes the database entries.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    Task InitializeDatabaseEntries(IServiceProvider serviceProvider);

    /// <summary>Imports the applications.</summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <param name="accountId"></param>
    /// <returns>
    ///   <br />
    /// </returns>
    Task ImportSeedData(IServiceProvider serviceProvider, string accountId);
}
