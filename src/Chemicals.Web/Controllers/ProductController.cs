using Chemicals.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chemicals.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductViewModelService _productViewModelService;

    public ProductController(IProductViewModelService productViewModelService)
    {
        _productViewModelService = productViewModelService;
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
}