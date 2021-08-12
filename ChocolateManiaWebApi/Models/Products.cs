using System;
using System.ComponentModel.DataAnnotations;

namespace ChocolateManiaWebApi.Models
{
    public class Products
    {
        [Key]
        public string Id { get; set; }
        public int Category { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double CalorieContent { get; set; }
        public bool? Sugarless { get; set; }
        public bool? Handmade { get; set; } 
        public string Manufacturer { get; set; }
        public int? InStock { get; set; }
        //TODO: Можно сделать подсчёт скидки
        public int? Discount { get; set; }
    }
}
