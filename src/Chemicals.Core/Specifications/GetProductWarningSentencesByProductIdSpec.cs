using Ardalis.Specification;
using Chemicals.Core.Entities.ChemicalAggregate;

namespace Chemicals.Core.Specifications;

public sealed class GetProductWarningSentencesByProductIdSpec : Specification<ProductWarningSentence>
{
    public GetProductWarningSentencesByProductIdSpec(int productId)
    {
        Query
            .Where(product => product.ProductId == productId);
    }
}