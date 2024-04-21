using Chemicals.Core.Interfaces.IntegrationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chemicals.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "IntegrationPolicy")]
public class ProductWsIntegrationController : ControllerBase
{
    private readonly IProductWsIntegrationService _productWsIntegrationService;

    public ProductWsIntegrationController(IProductWsIntegrationService productWsIntegrationService)
    {
        _productWsIntegrationService = productWsIntegrationService;
    }
    
    [HttpGet("active")]
    public async Task<IActionResult> GetActiveWarningSentences()
    {
        var result = await _productWsIntegrationService.GetActiveWarningSentences();
        return Ok(result);
    }
}