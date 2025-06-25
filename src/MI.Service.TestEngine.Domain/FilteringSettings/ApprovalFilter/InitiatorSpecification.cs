using System.Linq.Expressions;
using MI.Service.TestEngine.Domain.Entities;
using MI.Service.Shared.Common.Filtering.Common.Specification;

namespace MI.Service.TestEngine.Domain.FilteringSettings.TestFilter;

/// <inheritdoc />
public class InitiatorSpecification : Specification<Test>
{
    private readonly string initiator;

    /// <summary>
    /// Initializes a new instance of the <see cref="InitiatorSpecification"/> class.
    /// </summary>
    /// <param name="initiator">Test initiate by.</param>
    public InitiatorSpecification(string initiator)
    {
        this.initiator = initiator;
    }

    /// <inheritdoc />
    public override Expression<Func<Test, bool>> ToExpression()
    {
        return (Test) => Test.Initiator.Contains(this.initiator);
    }
}