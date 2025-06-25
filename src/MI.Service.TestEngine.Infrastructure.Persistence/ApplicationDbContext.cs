using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MI.Service.TestEngine.Domain.Entities;
using MI.Service.TestEngine.Domain.Entities.Base;
using MI.Service.TestEngine.Infrastructure.Persistence.Constants;
using MI.Service.TestEngine.Infrastructure.Persistence.EntityConfiguration;
using MI.Service.TestEngine.Infrastructure.Persistence.Extensions;
using MI.Service.TestEngine.Shared.Constants;
using MI.Service.TestEngine.Shared.Context;
using MI.Service.Shared.Constants.Account;
using MI.Service.Shared.Constants.Auth;

namespace MI.Service.TestEngine.Infrastructure.Persistence;

/// <summary>
/// Application DbContext
/// </summary>
public class ApplicationDbContext : DbContext
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly IAccountContext accountContext;
    /// <summary>
    /// The default system username.
    /// </summary>
    private const string SystemUser = DbContextConstant.SystemUser;

    /// <summary>
    /// The default database schema.
    /// </summary>
    public const string DefaultSchema = DbContextConstant.DefaultSchema;

    /// <summary>
    /// Gets or sets the Test rules.
    /// </summary>
    /// <value>The Rules.</value>
    p
    /// <summary>
    /// Gets or sets the Application.
    /// </summary>
    public DbSet<Application> Applications { get; set; }

    /// <summary>
    /// Gets or sets the Module.
    /// </summary>
    public DbSet<Module> Modules { get; set; }

    /// <summary>
    /// Gets or sets the documents.
    /// </summary>
    public DbSet<Document> Documents { get; set; }

    /// <summary>
    /// Gets or sets the Test attachments.
    /// </summary>
    public DbSet<TestAttachment> TestAttachments { get; set; }

    /// <summary>Initializes a new instance of the <see cref="ApplicationDbContext" /> class.</summary>
    /// <param name="options">The options.</param>
    /// <param name="accountContext"></param>
    /// <param name="httpContextAccessor"></param>
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IAccountContext accountContext,
        IHttpContextAccessor httpContextAccessor
        )
        : base(options)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.accountContext = accountContext;
        this.ChangeTracker.LazyLoadingEnabled = false;
    }

    /// <summary>
    /// Override this method to further configure the model that was discovered by convention
    /// from the entity types exposed in <see
    /// cref="T:Microsoft.EntityFrameworkCore.DbSet`1">DbSet</see> properties on your derived
    /// context. The resulting model may be cached and re-used for subsequent instances of your
    /// derived context.
    /// </summary>
    /// <param name="builder">
    /// The builder being used to construct the model for this context. Databases (and other
    /// extensions) typically define extension methods on this object that allow you to
    /// configure aspects of the model that are specific to a given database.
    /// </param>
    /// <remarks>
    /// If a model is explicitly set on the options for this context (via <see
    /// cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)">UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)</see>)
    /// then this method will not be run.
    /// </remarks>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema(DefaultSchema);

        

        this.ApplyGlobalFilter(builder);
    }

    /// <summary>Saves the entities asynchronous.</summary>
    /// <param name="hasAccountId"></param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Result of asynchronous operation.</returns>
    public async Task<int> SaveEntitiesAsync(bool hasAccountId = false, CancellationToken cancellationToken = default(CancellationToken))
    {
        
        return await this.SaveChangesAsync(cancellationToken);
    }

    private void ApplyGlobalFilter(ModelBuilder modelBuilder)
    {
        
    }


    private string GetCurrentUsername()
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


}
