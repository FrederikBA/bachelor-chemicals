using Ardalis.Specification;
using Chemicals.Core.Entities.ChemicalAggregate;

namespace Chemicals.Core.Specifications;

public sealed class GetProductByIdFullSpec : Specification<Product>
{
    public GetProductByIdFullSpec(int id)
    {
        Query
            .Where(product => product.Id == id)
            .Include(product => product.Producer)
            .ThenInclude(producer => producer!.Address)
            .Include(product => product.ProductStatus)
            .Include(product => product.ProductCategory)
            .ThenInclude(category => category!.ProductGroup);
    }
}