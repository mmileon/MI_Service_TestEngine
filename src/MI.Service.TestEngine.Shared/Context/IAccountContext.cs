namespace MI.Service.TestEngine.Shared.Context;

/// <summary>
///   <para>Account for Multitenance</para>
/// </summary>
public interface IAccountContext
{
    /// <summary>
    /// Gets the account name.
    /// </summary>
    public string CurrentAccountId { get; }

    /// <summary>
    /// Gets or sets account information.
    /// </summary>
    public AccountInfo CurrentAccount { get; set; }
}
