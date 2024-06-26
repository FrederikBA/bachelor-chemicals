using Chemicals.Web.ViewModels.Product;

namespace Chemicals.Web.Interfaces;

public interface IProductViewModelService
{
    public Task<List<ProductViewModel>> GetProductViewModelsAsync();
    public Task<ProductViewModel> GetProductViewModelAsync(int id);
}