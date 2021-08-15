using ChocolateMania.DI.Purchases;
using ChocolateMania.Models.PurchasesViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChocolateManiaWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchases _purchases;

        public PurchasesController(IPurchases purchases)
        {
            _purchases = purchases;
        }

        [HttpPost]
        public async Task<string> SoldProducts([FromBody] List<SoldProductViewModel> soldProducts) => JsonConvert.SerializeObject(await _purchases.SoldItems(soldProducts));
    }
}
