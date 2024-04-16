using Chemicals.Core.Interfaces;

namespace Chemicals.Core.Entities.ChemicalAggregate;

public class Product : BaseEntity, IAggregateRoot
{
    public string? Name { get; set; }
    public int ProductCategoryId { get; set; }
    public int ProducerId { get; set; }
    public int ProductStatusId { get; set; }
    public ProductCategory? ProductCategory { get; set; }
    public Producer? Producer { get; set; }
    public ProductStatus? ProductStatus { get; set; }
}