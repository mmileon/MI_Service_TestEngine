using System.Linq.Expressions;
using MI.Service.TestEngine.Domain.Entities;
using MI.Service.Shared.Common.Filtering.Common.Specification;

namespace MI.Service.TestEngine.Domain.FilteringSettings.RuleFilter;

/// <inheritdoc/>
public class CreatedBySpecification : Specification<Rule>
{
    private readonly string createdBy;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreatedBySpecification"/> class.
    /// </summary>
    /// <param name="createdBy">Test initiate by.</param>
    public CreatedBySpecification(string createdBy)
    {
        this.createdBy = createdBy;
    }

    /// <inheritdoc/>
    public override Expression<Func<Rule, bool>> ToExpression()
    {
        return (rule) => rule.CreatedBy.Contains(this.createdBy);
    }
}
