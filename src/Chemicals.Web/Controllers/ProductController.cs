using Chemicals.Core.Interfaces.DomainServices;
using Chemicals.Core.Models.Dtos;
using Chemicals.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chemicals.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductViewModelService _productViewModelService;
    private readonly IProductService _productService;

    public ProductController(IProductViewModelService productViewModelService, IProductService productService)
    {
        _productViewModelService = productViewModelService;
        _productService = productService;
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productViewModelService.GetProductViewModelsAsync();
        return Ok(products);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _productViewModelService.GetProductViewModelAsync(id);
        return Ok(product);
    }
    
    [HttpPost("add-warning-sentence")]
    public async Task<IActionResult> AddWarningSentence(AddWsDto dto)
    {
        var warningSentence = await _productService.AddWarningSentenceAsync(dto);
        return Ok(warningSentence);
    }
    
    [HttpPost("remove-warning-sentence")]
    public async Task<IActionResult> RemoveWarningSentence(RemoveWsDto dto)
    {
        var warningSentence = await _productService.RemoveWarningSentenceAsync(dto);
        return Ok(warningSentence);
    }
}