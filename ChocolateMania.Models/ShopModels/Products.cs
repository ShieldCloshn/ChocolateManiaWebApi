using System.ComponentModel.DataAnnotations;

namespace ChocolateMania.Models.ShopModels
{
    public class Products
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double CalorieContent { get; set; }
        public bool? Sugarless { get; set; }
        public bool? Handmade { get; set; } 
        public string Manufacturer { get; set; }
        public int? InStock { get; set; }
        //TODO: Можно сделать подсчёт скидки
        public int? Discount { get; set; }

        public int CategoryId { get; set; }
        public Categories Category { get; set; }
    }
}
