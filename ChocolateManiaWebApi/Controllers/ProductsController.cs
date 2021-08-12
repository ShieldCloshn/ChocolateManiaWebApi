using ChocolateManiaWebApi.Filters;
using ChocolateManiaWebApi.Models;
using ChocolateManiaWebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChocolateManiaWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        readonly ApiDBContext context;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ApiDBContext _context, ILogger<ProductsController> logger)
        {
            _logger = logger;
            context = _context;
        }

        //не работает
        [HttpGet(Name = "GetAllProducts")]
        public async Task<string> GetProducts([FromQuery] GetProductsFilter filters)
        {
            //var filters = JsonConvert.DeserializeObject<GetProductsFilter>(data);
            var products = context.Products.AsNoTracking();

            if (filters.Categories != null)
                products = products.AsNoTracking().Where(t => filters.Categories.Contains(t.Category));

            //TODO: Сейчас фильтрует по максимально-возможному кол-ву коллорий, можно сделать в промежутке от и до
            if ((filters.CalorieContent.HasValue))
                products = products.AsNoTracking().Where(t => t.CalorieContent <= filters.CalorieContent);

            if ((filters.Handmade.HasValue))
                products = products.AsNoTracking().Where(t => t.Handmade.Value == filters.Handmade.Value);

            //TODO: Сделать фильтр не только по наличию, но и по датам возможной доставки (в наличии с:) 
            if (filters.InStock.HasValue)
                products = products.AsNoTracking().Where(t => t.InStock > 0);

            //TODO: сейчас ищет просто по вхождению части названия производителя, возможно лучше реализовать так: часть названия или id поставщика
            if (!string.IsNullOrEmpty(filters?.Manufacturer))
                products = products.AsNoTracking().Where(t => t.Manufacturer.Contains(filters.Manufacturer));

            if (filters.Sugarless.HasValue)
                products = products.AsNoTracking().Where(t => t.Sugarless.Value == filters.Sugarless.Value);

            //TODO: сперва нужно реализовать список особенностей кондитерской продукции
            //if (filters.Peculiarities != null)
            //    products = products.AsNoTracking().Where(t => filters.Peculiarities.Contains(t.Peculiarities));

            //TODO: Сортировка по цене

            //TODO: сортировка по кол-ву в наличии

            var result = new ProductsViewModel
            {
                Products = await products.Skip((filters.Page - 1) * filters.Take).Take(filters.Take).ToListAsync(),
                TotalCount = await products.CountAsync(),
            };

            return JsonConvert.SerializeObject(result);
        }
 
        [HttpGet("{id}")]
        public async Task<string> Product(string id)
        {
            
            if (string.IsNullOrEmpty(id))
                return BadRequest().ToString();

            var product = await context.Products.SingleOrDefaultAsync(t => t.Id.Equals(id));
            return JsonConvert.SerializeObject(product);
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] Products newProduct)
        {
            if (newProduct != null)
            {
                //TODO: на уровне БД настроить проверку уникальности например по имени продукции
                newProduct.Id = Guid.NewGuid().ToString();
                context.Products.Add(newProduct);
                context.SaveChanges();
                return CreatedAtRoute("Product", new { id = newProduct.Id }, newProduct);
            }
            else return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(string id)
        {
            var deleteditem = context.Products.FirstOrDefault(t => t.Id.Equals(id));

            if (deleteditem == null)
                return BadRequest();

            context.Products.Remove(deleteditem);
            context.SaveChanges();

            return new ObjectResult(deleteditem);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(string id, [FromBody] Products updatedProduct)
        {
            if (updatedProduct == null || string.IsNullOrEmpty(id))
                return BadRequest();

            var currentProduct = context.Products.SingleOrDefault(t => t.Id.Equals(id));

            if (currentProduct == null)
                return BadRequest();

            currentProduct.CalorieContent = updatedProduct.CalorieContent;
            currentProduct.Category = updatedProduct.Category;
            currentProduct.Discount = updatedProduct.Discount;
            currentProduct.Handmade = updatedProduct.Handmade;
            currentProduct.InStock = updatedProduct.InStock;
            currentProduct.Manufacturer = updatedProduct.Manufacturer;
            currentProduct.Name = updatedProduct.Name;
            currentProduct.Price = updatedProduct.Price;
            currentProduct.Sugarless = updatedProduct.Sugarless;

            context.Products.Update(currentProduct);
            context.SaveChanges();
            return RedirectToRoute("Product");
        }  
    }
}
