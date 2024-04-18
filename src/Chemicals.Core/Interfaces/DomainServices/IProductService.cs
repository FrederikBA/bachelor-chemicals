using Chemicals.Core.Entities.ChemicalAggregate;

namespace Chemicals.Core.Interfaces.DomainServices;

public interface IProductService
{
    Task<List<Product>> GetAllProductsAsync();
}