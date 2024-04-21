using Chemicals.Web.ViewModels.WarningSentence;

namespace Chemicals.Web.ViewModels.Product;

public class ProductViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public ProductStatusViewModel? Status { get; set; }
    public ProducerViewModel? Producer { get; set; }
    public ProductCategoryViewModel? Category { get; set; }
    public List<WarningSentenceViewModel>? WarningSentences { get; set; }
}