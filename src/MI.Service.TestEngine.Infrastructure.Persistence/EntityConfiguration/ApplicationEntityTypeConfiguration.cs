using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MI.Service.TestEngine.Domain.Entities;
using MI.Service.TestEngine.Infrastructure.Persistence.EntityConfiguration.Base;
using MI.Service.Shared.Common.Multitenancy;

namespace MI.Service.TestEngine.Infrastructure.Persistence.EntityConfiguration;

/// <inheritdoc/>
/// <summary>
/// The application entity type configuration.
/// </summary>
public class ApplicationEntityTypeConfiguration : DomainEntityTypeConfiguration<Application>
{
    /// <inheritdoc/>
    public override string TableName => "Applications";

    /// <summary>
    /// The table name.
    /// </summary>
    public const string Table = "Applications";

    /// <inheritdoc />
    public ApplicationEntityTypeConfiguration(string providerName) : base(providerName)
    {
    }

    /// <inheritdoc/>
    public override void Configure(EntityTypeBuilder<Application> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValueSql(this.DefaultBooleanTrueValue);

        builder.HasIndex(x => new { x.Name, x.AccountId }).IsUnique();
    }
}
