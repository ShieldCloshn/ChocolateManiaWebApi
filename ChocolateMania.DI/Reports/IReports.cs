using ChocolateMania.Models.Reports;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChocolateMania.DI.Reports
{
    public interface IReports
    {
        public Task<List<ProductsReportView>> GetSoldProductsReport(DateTime? startDate, DateTime? endDate);
    }
}
