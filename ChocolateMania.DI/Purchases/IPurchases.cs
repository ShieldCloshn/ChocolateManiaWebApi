using ChocolateMania.Models.PurchasesViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChocolateMania.DI.Purchases
{
    public interface IPurchases
    {
        Task<List<SoldProductViewModel>> SoldItems(List<SoldProductViewModel> soldProducts);
    }
}
