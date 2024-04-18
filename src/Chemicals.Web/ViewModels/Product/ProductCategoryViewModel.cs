namespace Chemicals.Web.ViewModels.Product;

public class ProductCategoryViewModel
{
    public int Id { get; set; }
    public string? Category { get; set; }
    public ProductGroupViewModel? ProductGroup { get; set; }
}