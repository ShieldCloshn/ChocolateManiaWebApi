using ChocolateMania.DI.Shop;
using ChocolateMania.Models.ShopViewModels;
using ChocolateManiaWebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChocolateManiaWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductsController : ControllerBase
    {
        //TODO: добавить логгер
        private readonly IShop _shop;

        public ProductsController(IShop shop)
        {
            _shop = shop;
        }

        //TODO: Реализовать обработку исключений

        [HttpGet]
        public async Task<string> GetProducts([FromQuery] GetProductsFilter filters) => JsonConvert.SerializeObject(await _shop.GetProducts(filters));

        [HttpGet]
        public async Task<string> GetProduct(string id) => JsonConvert.SerializeObject(await _shop.GetProduct(id));

        [HttpPost]
        public async Task<string> AddProduct([FromBody] ProductViewModel newProduct) => JsonConvert.SerializeObject(await _shop.AddProduct(newProduct));

        [HttpDelete]
        public async Task<string> DeleteProduct(string id) => JsonConvert.SerializeObject(await _shop.DeleteProduct(id));

        [HttpPut]
        public async Task<string> UpdateProduct([FromBody] ProductViewModel updatedProduct) => JsonConvert.SerializeObject(await _shop.UpdateProduct(updatedProduct));

        [HttpPost]
        public async Task<string> SoldProducts([FromBody] List<SoldProductViewModel> soldProducts) => JsonConvert.SerializeObject(await _shop.SoldItems(soldProducts));
    }
}
