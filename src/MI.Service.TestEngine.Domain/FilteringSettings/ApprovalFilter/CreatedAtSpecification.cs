using System.Globalization;
using System.Linq.Expressions;
using MI.Service.TestEngine.Domain.Entities;
using MI.Service.Shared.Common.Filtering.Common.Specification;

namespace MI.Service.TestEngine.Domain.FilteringSettings.TestFilter;

/// <inheritdoc />
public class CreatedAtSpecification : Specification<Test>
{
    private readonly string createdAt;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreatedAtSpecification"/> class.
    /// </summary>
    /// <param name="createdAt">Test created at.</param>
    public CreatedAtSpecification(string createdAt)
    {
        this.createdAt = createdAt;
    }

    /// <inheritdoc />
    public override Expression<Func<Test, bool>> ToExpression()
    {
        return (Test) => Test.CreatedAt.ToString(CultureInfo.InvariantCulture).Contains(this.createdAt);
    }
}