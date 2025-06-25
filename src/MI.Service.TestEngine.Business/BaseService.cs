using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MI.Service.TestEngine.Infrastructure.External.AuthService.Models;
using MI.Service.Shared.Constants.Auth;

namespace MI.Service.TestEngine.Business;

/// <summary>
/// Provide common service for Test.
/// </summary>
public abstract class BaseService
{
    private const string SystemUser = "System";

    private readonly IServiceProvider serviceProvider;

    /// <summary>
    /// Provides http context accessor.
    /// </summary>
    private IHttpContextAccessor httpContextAccessor => this.serviceProvider.GetService<IHttpContextAccessor>();

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseService"/> class.
    /// </summary>
    protected BaseService(
        IServiceProvider serviceProvider
    )
    {
        this.serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Gets username of the logged in user.
    /// </summary>
    protected internal string GetCurrentUsername()
    {
        string fullNameClaimValue = null;

        if (this.httpContextAccessor?.HttpContext != null)
        {
            fullNameClaimValue = this.httpContextAccessor.HttpContext.User?.Claims
                .FirstOrDefault(p => String.Equals(ClaimNames.UserName, p.Type, StringComparison.OrdinalIgnoreCase))?
                .Value;
        }

        return !string.IsNullOrEmpty(fullNameClaimValue) ? fullNameClaimValue : SystemUser;
    }

    /// <summary>
    /// Gets fullname of the logged in user.
    /// </summary>
    /// <param name="username">The username of the logged in user.</param>
    /// <param name="users">The users list.</param>
    protected string GetFullName(string username, List<AuthResponseModel> users)
    {
        var user = users.FirstOrDefault(u => u.UserName == username);

        if (user != null)
        {
            return string.IsNullOrEmpty(user.FirstName) && string.IsNullOrEmpty(user.LastName) ? user.UserName : $"{user.FirstName} {user.LastName}";
        }

        return SystemUser;
    }
}
