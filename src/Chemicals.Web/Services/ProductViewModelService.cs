using Chemicals.Core.Interfaces.DomainServices;
using Chemicals.Web.Interfaces;
using Chemicals.Web.ViewModels.Product;

namespace Chemicals.Web.Services;

public class ProductViewModelService : IProductViewModelService
{
    private readonly IProductService _productService;

    public ProductViewModelService(IProductService productService)
    {
        _productService = productService;
    }
    
    public async Task<List<ProductViewModel>> GetProductViewModelsAsync()
    {
        var productEntities = await _productService.GetAllProductsAsync();

        var productViewModels = productEntities.Select(product => new ProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            //Product Status
            Status = new ProductStatusViewModel
            {
                Id = product.ProductStatus?.Id ?? 0,
                StatusName = product.ProductStatus?.StatusName,
                Text = product.ProductStatus?.Text
            },

            //Product Producer (and producer address)
            Producer = new ProducerViewModel
            {
                Id = product.Producer?.Id ?? 0,
                CompanyName = product.Producer?.CompanyName,
                PhoneNumber = product.Producer?.PhoneNumber,
                Address = new ProducerAddressViewModel()
                {
                    Address = product.Producer?.Address?.Address,
                    City = product.Producer?.Address?.City,
                    PostalCode = product.Producer?.Address?.PostalCode,
                    Country = product.Producer?.Address?.Country
                }
            },

            //Product Category
            Category = new ProductCategoryViewModel
            {
                Id = product.ProductCategory?.Id ?? 0,
                Category = product.ProductCategory?.Category,
                ProductGroup = new ProductGroupViewModel
                {
                    Id = product.ProductCategory?.ProductGroup?.Id ?? 0,
                    GroupName = product.ProductCategory?.ProductGroup?.GroupName,
                    Remarks = product.ProductCategory?.ProductGroup?.Remarks
                }
            }
        }).ToList();

        return productViewModels;
    }
}