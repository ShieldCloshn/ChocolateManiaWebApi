using ChocolateMania.DI.Shop;
using ChocolateMania.Models.ShopViewModels;
using ChocolateManiaWebApi.Filters;
using ChocolateManiaWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChocolateManiaWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        //TODO: добавить логгер
        private readonly IShop _shop;

        public ProductsController(IShop shop)
        {
            _shop = shop;
        }

        [HttpGet(Name = "GetAllProducts")]
        public async Task<string> GetProducts([FromQuery] GetProductsFilter filters)
        {
            return await _shop.GetProducts(filters); 
        }

        [HttpGet("{id}")]
        public async Task<string> GetProduct(string id)
        {
            return await _shop.GetProduct(id);
        }

        [HttpPost]
        public async Task<string> AddProduct([FromBody] Products newProduct)
        {
            return await _shop.AddProduct(newProduct);
        }

        [HttpDelete("{id}")]
        public async Task<string> DeleteProduct(string id)
        {
            return await _shop.DeleteProduct(id);
        }

        [HttpPut]
        public async Task<string> UpdateProduct([FromBody] ProductViewModel updatedProduct)
        {
            return await _shop.UpdateProduct(updatedProduct);
        }
    }
}
