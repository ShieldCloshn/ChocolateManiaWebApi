using AutoMapper;
using ChocolateMania.Data;
using ChocolateMania.Models.PurchasesViewModels;
using ChocolateMania.Models.ShopModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChocolateMania.DI.Purchases
{
    public class Purchases : IPurchases
    {
        private readonly ApiDBContext context;

        public Purchases(ApiDBContext _context)
        {
            context = _context;
        }

        //TODO:Вынести конфигурацию маппера в отдельный класс
        public async Task<List<SoldProductViewModel>> SoldItems(List<SoldProductViewModel> soldItems)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<SoldProductViewModel, SoldProducts>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(scr => scr.Id))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString())));

            var mapper = new Mapper(config);
            var products = mapper.Map<List<SoldProductViewModel>, List<SoldProducts>>(soldItems);

            foreach (var product in products)
            {
                var currentProduct = await context.Products.FirstOrDefaultAsync(t => t.Id.Equals(product.ProductId));

                if (currentProduct.InStock > 0)
                {
                    currentProduct.InStock--;
                    await context.SoldProducts.AddAsync(product);
                    await context.SaveChangesAsync();
                }
            }

            return soldItems;
        }
    }
}
