using Chemicals.Core.Entities.ChemicalAggregate;
using Chemicals.Core.Exceptions;
using Chemicals.Core.Interfaces.DomainServices;
using Chemicals.Core.Interfaces.Repositories;
using Chemicals.Core.Specifications;

namespace Chemicals.Core.Services;

public class ProductService : IProductService
{
    private readonly IReadRepository<Product> _productReadRepository;

    public ProductService(IReadRepository<Product> productReadRepository)
    {
        _productReadRepository = productReadRepository;
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        var products = await _productReadRepository.ListAsync(new GetProductsFullSpec());
        
        if (products == null || products.Count == 0)
        {
            throw new ProductsNotFoundException();
        }
        
        return products;
    }

    public Task<Product> GetProductByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}