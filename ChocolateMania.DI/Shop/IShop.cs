using ChocolateMania.Models.ShopViewModels;
using ChocolateManiaWebApi.Filters;
using ChocolateManiaWebApi.Models;
using System.Threading.Tasks;

namespace ChocolateMania.DI.Shop
{
    public interface IShop
    {
        Task<string> GetProducts(GetProductsFilter filters);
        Task<string> GetProduct(string id);
        Task<string> AddProduct(Products newProduct);
        Task<string> DeleteProduct(string id);
        Task<string> UpdateProduct(ProductViewModel updatedProduct);
    }
}
