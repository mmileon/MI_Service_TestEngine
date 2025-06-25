using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MI.Service.TestEngine.Domain.Entities.Base;
using MI.Service.TestEngine.Infrastructure.Persistence.EntityConfiguration.Mapping;

namespace MI.Service.TestEngine.Infrastructure.Persistence.EntityConfiguration.Base;

/// <inheritdoc/>
public abstract class DomainEntityTypeConfiguration<T> : IEntityTypeConfiguration<T>
    where T : DomainEntity
{
    /// <summary>
    /// Gets the name of the table.
    /// </summary>
    public abstract string TableName { get; }

    /// <summary>
    /// Gets or sets default bool value.
    /// </summary>
    public string DefaultBooleanTrueValue => DefaultBoolValueMapping.DefaultBoolTrueValueMapping[this.ProviderName];

    /// <summary>
    /// Gets the provider name.
    /// </summary>
    public string ProviderName { get; }

    /// <summary>
    /// Initializes an instance of <see cref="DomainEntityTypeConfiguration{T}"/>.
    /// </summary>
    /// <param name="providerName">The provider name.</param>
    protected DomainEntityTypeConfiguration(string providerName)
    {
        this.ProviderName = providerName;
    }

    /// <inheritdoc/>
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.ToTable(this.TableName, ApplicationDbContext.DefaultSchema).HasKey(x => x.SystemName);

        builder.Property(x => x.SystemName).ValueGeneratedOnAdd();
    }
}
