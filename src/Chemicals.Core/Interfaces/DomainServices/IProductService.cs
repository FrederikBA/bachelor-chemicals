using Chemicals.Core.Entities.ChemicalAggregate;
using Chemicals.Core.Models.Dtos;

namespace Chemicals.Core.Interfaces.DomainServices;

public interface IProductService
{
    Task<List<Product>> GetAllProductsAsync();
    Task<Product> GetProductByIdAsync(int id);
    Task<ProductWarningSentence> AddWarningSentenceAsync(AddWsDto dto);
}