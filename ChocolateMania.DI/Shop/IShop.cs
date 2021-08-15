using ChocolateMania.Models.ShopViewModels;
using ChocolateManiaWebApi.Filters;
using ChocolateManiaWebApi.ViewModels;
using System.Threading.Tasks;

namespace ChocolateMania.DI.Shop
{
    public interface IShop
    {
        Task<ProductsViewModel> GetProducts(GetProductsFilter filters);
        Task<ProductViewModel> GetProduct(string id);
        Task<ProductViewModel> AddProduct(ProductViewModel newProduct);
        Task<bool> DeleteProduct(string id);
        Task<bool> UpdateProduct(ProductViewModel updatedProduct);
 
    }
}
