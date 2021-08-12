using ChocolateManiaWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChocolateManiaWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController: ControllerBase
    {
        readonly ApiDBContext context;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ApiDBContext _context, ILogger<ProductsController> logger)
        {
            _logger = logger;
            context = _context;
        }


        [HttpGet]
        public async Task<List<Products>> GetProducts()
        {
            var result = context.Products.ToList();
            return result;        
        }

        //[HttpPost]
        //public async Task<Products> UpdateProduct()
        //{


        //}

        //[HttpPost]
        //public async Task<Products> AddProduct()
        //{
            
        //}

    }
}
