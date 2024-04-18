using Chemicals.Core.Entities.ChemicalAggregate;
using Chemicals.Core.Interfaces.DomainServices;
using Chemicals.Core.Interfaces.Repositories;

namespace Chemicals.Core.Services;

public class ProductService : IProductService
{
    private readonly IReadRepository<Product> _productReadRepository;

    public ProductService(IReadRepository<Product> productReadRepository)
    {
        _productReadRepository = productReadRepository;
    }

    public Task<List<Product>> GetAllProductsAsync()
    {
        throw new NotImplementedException();
    }
}