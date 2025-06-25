using MI.Service.TestEngine.Domain.Entities.Base;

namespace MI.Service.TestEngine.Domain.Entities;

/// <summary>
/// Entity for Application.
/// </summary>
public class Application : DomainEntity, IMultiTenancyEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Application"/> class.
    /// </summary>
    public Application()
    { }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the active status.
    /// </summary>
    /// <value>The active status.</value>
    public bool IsActive { get; set; }

    
}
