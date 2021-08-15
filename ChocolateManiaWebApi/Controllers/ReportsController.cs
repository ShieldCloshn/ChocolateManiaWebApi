using ChocolateMania.DI.Reports;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace ChocolateManiaWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReports _reports;

        public ReportsController(IReports reports)
        {
            _reports = reports;
        }

        [HttpPost]
        public async Task<string> GetSalesReport(DateTime? startDate, DateTime? endDate) => JsonConvert.SerializeObject(await _reports.GetSoldProductsReport(startDate, endDate));
    }
}
