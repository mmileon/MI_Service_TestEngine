using System.Linq.Expressions;
using MI.Service.TestEngine.Domain.Entities;
using MI.Service.Shared.Common.Filtering.Common.Specification;

namespace MI.Service.TestEngine.Domain.FilteringSettings.TestFilter;

/// <inheritdoc />
public class TestStatusSpecification : Specification<Test>
{
    private readonly string status;

    /// <summary>
    /// Initializes a new instance of the <see cref="TestStatusSpecification"/> class.
    /// </summary>
    /// <param name="status">Test status.</param>
    public TestStatusSpecification(string status)
    {
        this.status = status;
    }

    /// <inheritdoc />
    public override Expression<Func<Test, bool>> ToExpression()
    {
        return Test => Test.TestStatus.ToString().ToLower().Contains(this.status.ToLower());
    }
}