using System.Linq.Expressions;
using MI.Service.TestEngine.Domain.Entities;
using MI.Service.Shared.Common.Filtering.Common.Specification;

namespace MI.Service.TestEngine.Domain.FilteringSettings.TestFilter;

/// <inheritdoc />
public class ModuleSpecification : Specification<Test>
{
    private readonly string module;

    /// <summary>
    /// Initializes a new instance of the <see cref="ModuleSpecification"/> class.
    /// </summary>
    /// <param name="module">Module name.</param>
    public ModuleSpecification(string module)
    {
        this.module = module;
    }

    /// <inheritdoc />
    public override Expression<Func<Test, bool>> ToExpression()
    {
        return Test => Test.Module.Name.Contains(this.module);
    }
}