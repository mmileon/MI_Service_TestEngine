namespace MI.Service.TestEngine.Domain.Views;

/// <summary>
/// Application and module view
/// </summary>
public class ApplicationModuleView
{
    /// <summary>
    /// Gets or sets the name of the application system.
    /// </summary>
    public Guid ApplicationSystemName { get; set; }

    /// <summary>
    /// Gets or sets the name of the module system.
    /// </summary>
    public Guid? ModuleSystemName { get; set; }

    /// <summary>
    /// Gets or sets the name of the application.
    /// </summary>
    public string ApplicationName { get; set; }

    /// <summary>
    /// Gets or sets the name of the module.
    /// </summary>
    public string ModuleName { get; set; }
}