using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ChocolateMania.Models.ShopModels
{
    public class SoldProducts
    {
        public string Id { get; set; }
        public string CheckData { get; set; }
        public DateTime SoldDate { get; set; }
        public string ShopAddress { get; set; }

        public string ProductId { get; set; }
        public Products Product { get; set; }

    }
}
