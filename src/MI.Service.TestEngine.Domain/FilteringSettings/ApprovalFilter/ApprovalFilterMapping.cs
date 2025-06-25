using MI.Service.TestEngine.Domain.Entities;
using MI.Service.Shared.Common.Filtering.Common.Specification;
using MI.Service.Shared.Common.Filtering.EfQueryBuilder;

namespace MI.Service.TestEngine.Domain.FilteringSettings.TestFilter;

/// <inheritdoc />
public class TestFilterMapping : IFilterExpressionMapping<Test>
{
    private const string Name = "name";
    private const string RequestedOn = "requestedon";
    private const string Initiator = "initiator";
    private const string Status = "status";
    private const string App = "app";
    private const string Module = "module";
    private const string ModuleFieldName = "Test.Application.Module.Name";
    private const string ApplicationFieldName = "Test.Application.Name";

    /// <inheritdoc />
    public Specification<Test> FilteringMapping(string property, string searchTerm)
    {
        var defaultSpecification = new OrSpecification<Test>(
            new NameSpecification(searchTerm),
            new OrSpecification<Test>(
                new OrSpecification<Test>(
                    new ApplicationSpecification(searchTerm),
                    new ModuleSpecification(searchTerm)),
                new InitiatorSpecification(searchTerm))
        );

        return property?.ToLowerInvariant() switch
        {
            Name => new NameSpecification(searchTerm),
            Initiator => new InitiatorSpecification(searchTerm),
            RequestedOn => new CreatedAtSpecification(searchTerm),
            Status => new TestStatusSpecification(searchTerm),
            App => new ApplicationSpecification(searchTerm),
            Module => new ModuleSpecification(searchTerm),
            _ => defaultSpecification
        };
    }

    /// <inheritdoc />
    public string OrderByMapping(string orderBy)
    {
        return orderBy?.ToLowerInvariant() switch
        {
            Name => Name,
            Initiator => nameof(Test.Initiator),
            RequestedOn => nameof(Test.CreatedAt),
            Status => nameof(Test.TestStatus),
            App => ApplicationFieldName,
            Module => ModuleFieldName,
            _ => nameof(Test.CreatedAt)
        };
    }
}