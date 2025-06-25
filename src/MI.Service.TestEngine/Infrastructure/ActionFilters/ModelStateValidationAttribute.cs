using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MI.Service.TestEngine.Contracts;

namespace MI.Service.TestEngine.Infrastructure.ActionFilters;

/// <summary>
/// Custom ModelValidationAttribute
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class ModelStateValidationAttribute : ActionFilterAttribute
{
    /// <inheritdoc />
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var response = new ErrorResponse
            {
                Messages = context.ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToList(),
            };
            context.Result = new BadRequestObjectResult(response);
        }
    }
}