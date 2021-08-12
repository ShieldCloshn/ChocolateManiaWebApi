using System;

namespace ChocolateManiaWebApi.Models
{
    public class Products
    {
        public int Id { get; set; }
        public int Category { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double CalorieContent { get; set; }
        public bool? Sugarless { get; set; }
        public bool? Handmade { get; set; } 
        public string Manufacturer { get; set; }
        public int? InStock { get; set; }
        //через конструктор можно считать скидку для постоянных клиентов или спец. предложения
        public int? Discount { get; set; }
        //сделать List особенностей
        public int? Peculiarities { get; set; }
    }
}
