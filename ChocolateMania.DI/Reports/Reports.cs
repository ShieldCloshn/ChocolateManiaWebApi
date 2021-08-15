using ChocolateMania.Data;
using ChocolateMania.Models.Reports;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChocolateMania.DI.Reports
{
    public class Reports : IReports
    {
        private readonly ApiDBContext context;

        public Reports(ApiDBContext _context)
        {
            context = _context;
        }
        //TODO: можно реализовать выгрузку отчётов в Excel
        public async Task<List<ProductsReportView>> GetSoldProductsReport(DateTime? startDate, DateTime? endDate)
        {
            var soldProducts = context.SoldProducts.AsNoTracking();

            if (!startDate.HasValue && !endDate.HasValue)
            {
                soldProducts = context.SoldProducts.AsNoTracking().Where(t => t.SoldDate >= DateTime.Now.AddMonths(-1));
            }

            if (startDate.HasValue)
            {
                soldProducts = context.SoldProducts.AsNoTracking().Where(t => t.SoldDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                soldProducts = context.SoldProducts.AsNoTracking().Where(t => t.SoldDate <= endDate.Value);
            }

            var soldProductsList = await soldProducts.Include(u => u.Product).ThenInclude(x => x.Category).ToListAsync();
            var report = new List<ProductsReportView>();

            foreach (var product in soldProductsList)
            {
                var countSoldProducts = soldProductsList.Count(t => t.ProductId == product.ProductId);
                var totalSum = soldProducts.Where(t => t.ProductId.Equals(product.ProductId)).Sum(t => t.Product.Price);
                var addReportProduct = new ProductsReportView(product.ProductId, product.Product.Name, product.Product.Category.Name, countSoldProducts, product.Product.InStock.Value, totalSum);
                report.Add(addReportProduct);
            }

            report = report.Distinct().ToList();
            return report;
        }
    }
}
