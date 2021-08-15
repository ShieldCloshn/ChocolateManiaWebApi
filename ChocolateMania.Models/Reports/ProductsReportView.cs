using System;

namespace ChocolateMania.Models.Reports
{
    //для работы .Disctint реализуем интерфейс IEquatable
    public class ProductsReportView : IEquatable<ProductsReportView>
    {
        public ProductsReportView() 
        {
        }

        //TODO: вомзоожно лучше проверять не только по Id продукта, но и по другим полям
        public bool Equals(ProductsReportView other)
        {
            if (ProductId == other.ProductId)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hashProductId = ProductId == null ? 0 : ProductId.GetHashCode();
            return hashProductId;
        }

        public ProductsReportView(string productId, string productName, string categoryName, int quantitySold, int inStock, decimal totalSum)
        {
            ProductId = productId;
            ProductName = productName;
            CategoryName = categoryName;
            QuantitySold = quantitySold;
            InStock = inStock;
            TotalSum = totalSum;
        }

        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public int QuantitySold { get; set; }
        public int InStock { get; set; }
        public decimal TotalSum { get; set; }
    }
}
