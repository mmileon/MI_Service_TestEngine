using MI.Service.TestEngine.Domain.Entities;
using MI.Service.Shared.Common.Filtering.Common.Specification;
using MI.Service.Shared.Common.Filtering.EfQueryBuilder;

namespace MI.Service.TestEngine.Domain.FilteringSettings.RuleFilter;

/// <inheritdoc/>
public class RuleFilterMapping : IFilterExpressionMapping<Rule>
{
    private const string Name = "name";
    private const string Application = "app";
    private const string Module = "module";
    private const string CreatedBy = "createdBy";

    /// <inheritdoc/>
    public Specification<Rule> FilteringMapping(string property, string searchTerm)
    {
        var defaultSpecification = new OrSpecification<Rule>(
            new NameSpecification(searchTerm),
            new OrSpecification<Rule>(
                new ApplicationSpecification(searchTerm),
                new OrSpecification<Rule>(
                    new ModuleSpecification(searchTerm),
                    new CreatedBySpecification(searchTerm)
                )
            )
        );

        return property?.ToLowerInvariant() switch
        {
            Name => new NameSpecification(searchTerm),
            Application => new ApplicationSpecification(searchTerm),
            Module => new ModuleSpecification(searchTerm),
            CreatedBy => new CreatedBySpecification(searchTerm),
            _ => defaultSpecification
        };
    }

    /// <inheritdoc/>
    public string OrderByMapping(string orderBy)
    {
        return orderBy?.ToLowerInvariant() switch
        {
            _ => nameof(Rule.LastModifiedAt)
        };
    }
}