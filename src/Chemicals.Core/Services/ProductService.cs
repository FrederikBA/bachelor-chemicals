using Chemicals.Core.Entities.ChemicalAggregate;
using Chemicals.Core.Interfaces.DomainServices;

namespace Chemicals.Core.Services;

public class ProductService : IProductService
{
    public Task<List<Product>> GetAllProductsAsync()
    {
        throw new NotImplementedException();
    }
}