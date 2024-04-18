using Ardalis.Specification;
using Chemicals.Core.Entities.ChemicalAggregate;

namespace Chemicals.Core.Specifications;

public sealed class GetProductsFullSpec : Specification<Product>
{
    public GetProductsFullSpec()
    {
        Query
            .Include(product => product.Producer)
            .ThenInclude(producer => producer!.Address)
            .Include(product => product.ProductStatus)
            .Include(product => product.ProductCategory)
            .ThenInclude(category => category!.ProductGroup);
    }
}