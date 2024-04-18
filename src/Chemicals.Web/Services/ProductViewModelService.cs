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

    public async Task<ProductViewModel> GetProductViewModelAsync(int id)
    {
        var productEntity = await _productService.GetProductByIdAsync(id);

        var productViewModel = new ProductViewModel
        {
            Id = productEntity.Id,
            Name = productEntity.Name,
            //Product Status
            Status = new ProductStatusViewModel
            {
                Id = productEntity.ProductStatus?.Id ?? 0,
                StatusName = productEntity.ProductStatus?.StatusName,
                Text = productEntity.ProductStatus?.Text
            },

            //Product Producer (and producer address)
            Producer = new ProducerViewModel
            {
                Id = productEntity.Producer?.Id ?? 0,
                CompanyName = productEntity.Producer?.CompanyName,
                PhoneNumber = productEntity.Producer?.PhoneNumber,
                Address = new ProducerAddressViewModel()
                {
                    Address = productEntity.Producer?.Address?.Address,
                    City = productEntity.Producer?.Address?.City,
                    PostalCode = productEntity.Producer?.Address?.PostalCode,
                    Country = productEntity.Producer?.Address?.Country
                }
            },

            //Product Category
            Category = new ProductCategoryViewModel
            {
                Id = productEntity.ProductCategory?.Id ?? 0,
                Category = productEntity.ProductCategory?.Category,
                ProductGroup = new ProductGroupViewModel
                {
                    Id = productEntity.ProductCategory?.ProductGroup?.Id ?? 0,
                    GroupName = productEntity.ProductCategory?.ProductGroup?.GroupName,
                    Remarks = productEntity.ProductCategory?.ProductGroup?.Remarks
                }
            }
        };
        return productViewModel;
    }
}