using System.Collections;
using Microsoft.AspNetCore.Mvc;
using MI.Service.TestEngine.Business.Translation.Export;
using MI.Service.TestEngine.Contracts;

namespace MI.Service.TestEngine.Controllers;

/// <summary>
/// The translation controller.
/// </summary>
[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
[Route("api/v1/translations")]
public class TranslationController : ControllerBase
{
    private readonly ITranslationExportService translationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="TranslationController" /> class.
    /// </summary>
    /// <param name="translationService">The translation service.</param>
    public TranslationController(ITranslationExportService translationService)
    {
        this.translationService = translationService;
    }

    /// <summary>
    /// Exports translation for Test app.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="IDictionary{TKey,TValue}"/>.
    /// </returns>
    [HttpGet]
    [Produces("application/json")]
    [Route("export")]
    [ProducesResponseType(typeof(IDictionary), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IDictionary> Export()
    {
        return await this.translationService.ExportTestTranslation();
    }
}
