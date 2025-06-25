using System.Linq.Expressions;
using MI.Service.TestEngine.Domain.Entities;
using MI.Service.Shared.Common.Filtering.Common.Specification;

namespace MI.Service.TestEngine.Domain.FilteringSettings.TestFilter;

/// <inheritdoc />
public class NameSpecification : Specification<Test>
{
    private readonly string name;

    /// <summary>
    /// Initializes a new instance of the <see cref="NameSpecification"/> class.
    /// </summary>
    /// <param name="name">Test name.</param>
    public NameSpecification(string name)
    {
        this.name = name;
    }

    /// <inheritdoc />
    public override Expression<Func<Test, bool>> ToExpression()
    {
        return Test => Test.Name.Contains(this.name);
    }
}