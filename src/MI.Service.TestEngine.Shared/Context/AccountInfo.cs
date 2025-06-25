namespace MI.Service.TestEngine.Shared.Context;

/// <summary>
/// The account information.
/// </summary>
public class AccountInfo
{
    /// <summary>
    /// Gets or sets the account system name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AccountInfo"/> class.
    /// </summary>
    /// <param name="name">The account system name.</param>
    public AccountInfo(string name)
    {
        this.Name = name;
    }
}
