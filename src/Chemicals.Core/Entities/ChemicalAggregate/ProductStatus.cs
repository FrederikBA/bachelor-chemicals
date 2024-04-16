namespace Chemicals.Core.Entities.ChemicalAggregate;

public class ProductStatus : BaseEntity
{
    public string? StatusName { get; set; }
    public string? Text { get; set; }
    public int SortOrder { get; set; }
    public List<Product>? Products { get; set; }
}