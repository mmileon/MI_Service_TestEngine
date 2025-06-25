using MI.Service.Shared.Constants.Account;

namespace MI.Service.TestEngine.Shared.Context;

/// <summary>
///   <br />
/// </summary>
public class AccountContext : IAccountContext
{
    private AccountInfo currentAccount;

    /// <inheritdoc/>
    public string CurrentAccountId
    {
        get
        {
            this.currentAccount ??= new AccountInfo(AccountConstants.SystemAccount);
            return this.currentAccount.Name;
        }
    }

    /// <inheritdoc/>
    public AccountInfo CurrentAccount
    {
        get { return this.currentAccount; }

        set { this.currentAccount = value; }
    }
}
