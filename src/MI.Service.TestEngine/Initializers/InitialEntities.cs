using MI.Service.TestEngine.Domain.Entities;

namespace MI.Service.TestEngine.Initializers;

/// <summary>
/// Provide model for get initial entities.
/// </summary>
public class InitialEntities
{
    /// <summary>
    /// Gets or sets initial application.
    /// </summary>
    public IReadOnlyCollection<Application> Applications { get; set; }

    /// <summary>
    /// Gets or sets initial module.
    /// </summary>
    public IReadOnlyCollection<Module> Modules { get; set; }
}
