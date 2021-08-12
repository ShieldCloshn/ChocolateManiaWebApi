using ChocolateManiaWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChocolateManiaWebApi.ViewModels
{
    public class ProductsViewModel
    {
        public ProductsViewModel()
        {
            Products = new List<Products>();
        }
        public List<Products> Products { get; set; }
        public int TotalCount { get; set; }
    }
}
