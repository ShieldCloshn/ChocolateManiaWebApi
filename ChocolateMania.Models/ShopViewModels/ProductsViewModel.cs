using ChocolateManiaWebApi.Models;
using System.Collections.Generic;

namespace ChocolateManiaWebApi.ViewModels
{
    public class ProductsViewModel
    {
        public ProductsViewModel()
        {
        }

        public ProductsViewModel(List<Products> products, int totalCount)
        {
            Products = products;
            TotalCount = totalCount;
        }

        public List<Products> Products { get; set; }
        public int TotalCount { get; set; }
    }
}
