using System.Linq.Expressions;
using MI.Service.TestEngine.Domain.Entities;
using MI.Service.Shared.Common.Filtering.Common.Specification;

namespace MI.Service.TestEngine.Domain.FilteringSettings.TestFilter;

/// <inheritdoc />
public class ApplicationSpecification : Specification<Test>
{
    private readonly string app;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationSpecification"/> class.
    /// </summary>
    /// <param name="app">Application name.</param>
    public ApplicationSpecification(string app)
    {
        this.app = app;
    }

    /// <inheritdoc />
    public override Expression<Func<Test, bool>> ToExpression()
    {
        return Test => Test.Application.Name.Contains(this.app);
    }
}