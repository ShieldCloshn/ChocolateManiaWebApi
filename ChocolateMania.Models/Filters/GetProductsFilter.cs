using System.Collections.Generic;


namespace ChocolateManiaWebApi.Filters
{
    public class GetProductsFilter
    {
        public decimal? Price { get; set; }
        public bool? InStock { get; set; }
        public bool? Handmade { get; set; }
        public string Manufacturer { get; set; }
        public bool? Sugarless { get; set; }
        public List<int> Categories { get; set; }
        public double? CalorieContent { get; set; }
        public int Take { get; set; }
        public int Page { get; set; }
    }
}
