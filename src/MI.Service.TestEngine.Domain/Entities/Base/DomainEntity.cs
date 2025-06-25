namespace MI.Service.TestEngine.Domain.Entities.Base;

/// <summary>
/// Base entity for system name database object.
/// </summary>
public abstract class DomainEntity
{
    /// <summary>
    /// Gets or sets the system name.
    /// </summary>
    public virtual Guid SystemName { get; set; }
}