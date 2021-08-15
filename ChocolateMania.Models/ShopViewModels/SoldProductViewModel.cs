using System;

namespace ChocolateMania.Models.ShopViewModels
{
    public class SoldProductViewModel
    {
        public string Id { get; set; }
        public string CheckData { get; set; }
        public DateTime SoldDate { get; set; }
        public string ShopAddress { get; set; }
        //TODO: Проверка типа оплаты. Наличный, безналичный расчёт, сохранение реквизитов банка при б.н. расчёте
    }
}
