namespace MI.Service.TestEngine.Domain.Entities.Base;

/// <summary>IMultiTenancyEntity</summary>
public interface IMultiTenancyEntity
{
    /// <summary>Gets or sets the account identifier.</summary>
    /// <value>The account identifier.</value>
    string AccountId { get; set; }
}