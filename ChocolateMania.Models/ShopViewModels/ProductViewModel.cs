using System;
using System.Collections.Generic;
using System.Text;

namespace ChocolateMania.Models.ShopViewModels
{
    public class ProductViewModel
    {
        public string Id { get; set; }
        public int Category { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double CalorieContent { get; set; }
        public bool? Sugarless { get; set; }
        public bool? Handmade { get; set; }
        public string Manufacturer { get; set; }
        public int? InStock { get; set; }
        public int? Discount { get; set; }
    }
}
