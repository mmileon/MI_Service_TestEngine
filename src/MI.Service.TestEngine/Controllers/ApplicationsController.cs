using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MI.Service.TestEngine.Business.Applications;
using MI.Service.TestEngine.Business.Applications.Models;
using MI.Service.TestEngine.Business.Models;
using MI.Service.TestEngine.Constants;
using MI.Service.TestEngine.Contracts;
using MI.Service.TestEngine.Infrastructure.ActionFilters;

namespace MI.Service.TestEngine.Controllers;

/// <summary>
/// Application controller.
/// </summary>
[ApiController]
[ModelStateValidation]
[ApiExplorerSettings(GroupName = GroupNameConstants.Internal)]
[Route($"{RouteSchemaConstants.InternalApiRoutePath}/applications")]
public class ApplicationsController : ControllerBase
{
    private readonly ApplicationService applicationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationsController"/> class.
    /// </summary>
    /// <param name="applicationService"></param>
    public ApplicationsController(ApplicationService applicationService)
    {
        this.applicationService = applicationService;
    }

    /// <summary>
    /// Get rule application options.
    /// </summary>
    /// <returns>The list of rule applications.</returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<NameModel>), StatusCodes.Status200OK)]
    public async Task<List<NameModel>> GetApplicationsAsync()
    {
        return await applicationService.GetApplicationsAsync();
    }

    /// <summary>
    /// Get application module options.
    /// </summary>
    /// <returns>The list of rule application modules.</returns>
    [HttpGet("{name}/modules")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<NameModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<List<NameModel>> GetModulesAsync([FromRoute][Required] string name)
    {
        return await applicationService.GetModulesAsync(name);
    }

    /// <summary>
    /// Get application module options.
    /// </summary>
    /// <returns>The list of rule application modules.</returns>
    [HttpGet("modules")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<ApplicationModuleModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<List<ApplicationModuleModel>> GetApplicationModulesAsync()
    {
        return await applicationService.GetApplicationModulesAsync();
    }

    /// <summary>
    /// Creates the rule application.
    /// </summary>
    /// <param name="applicationModel">The rule model.</param>
    /// <returns></returns>
    [HttpPost("modules")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(NameModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    public async Task<NameModel> CreateApplicationModuleAsync([FromBody] ApplicationModuleModel applicationModel)
    {
        return await applicationService.CreateApplicationModulesAsync(applicationModel);
    }

    /// <summary>
    /// Creates the application asynchronous.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns></returns>
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(NameModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    public async Task<NameModel> CreateApplicationAsync([FromBody] NameModel model)
    {
        return await applicationService.CreateApplicationAsync(model);
    }

    /// <summary>
    /// Adds the module asynchronous.
    /// </summary>
    /// <param name="name">Name of the application.</param>
    /// <param name="model">The model.</param>
    /// <returns></returns>
    [HttpPost("{name}/modules")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(NameModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    public async Task<NameModel> AddModuleAsync([FromRoute][Required] string name, [FromBody] NameModel model)
    {
        return await applicationService.AddModuleAsync(name, model);
    }

    /// <summary>
    /// Renames the application asynchronous.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="model">The model.</param>
    /// <returns></returns>
    [HttpPatch("{name}")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(NameModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    public async Task<NameModel> RenameApplicationAsync([FromRoute][Required] string name, [FromBody] NameModel model)
    {
        return await applicationService.RenameApplicationAsync(name, model);
    }

    /// <summary>
    /// Renames the module asynchronous.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="model">The model.</param>
    /// <returns></returns>
    [HttpPatch("{name}/modules/{moduleName}")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(NameModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    public async Task<NameModel> RenameModuleAsync([FromRoute][Required] string name, [FromRoute][Required] string moduleName, [FromBody] NameModel model)
    {
        return await applicationService.RenameModuleAsync(name, moduleName, model);
    }

    /// <summary>
    /// Deletes the application asynchronous.
    /// </summary>
    /// <param name="name">The name.</param>
    [HttpDelete("{name}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task DeleteApplicationAsync([FromRoute][Required] string name)
    {
        await applicationService.DeleteApplicationAsync(name);
    }

    /// <summary>
    /// Deletes the module asynchronous.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="moduleName">Name of the module.</param>
    [HttpDelete("{name}/modules/{moduleName}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task DeleteModuleAsync([FromRoute][Required] string name, string moduleName)
    {
        await applicationService.DeleteModuleAsync(name, moduleName);
    }
}