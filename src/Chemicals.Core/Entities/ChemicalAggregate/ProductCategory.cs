using Shared.Integration.Models;

namespace Chemicals.Core.Entities.ChemicalAggregate;

public class ProductCategory : BaseEntity
{
    public int ProductGroupId { get; set; }
    public string? Category { get; set; }
    public List<Product>? Products { get; set; }
    public ProductGroup? ProductGroup { get; set; }
}