using AutoMapper;
using ChocolateMania.Data;
using ChocolateMania.Models.ShopViewModels;
using ChocolateManiaWebApi.Filters;
using ChocolateManiaWebApi.Models;
using ChocolateManiaWebApi.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ChocolateMania.DI.Shop
{
    public class Shop : IShop
    {
        private readonly ApiDBContext context;

        public Shop(ApiDBContext _context)
        {
            context = _context;
        }

        public async Task<string> GetProducts(GetProductsFilter filters)
        {
            var products = context.Products.AsNoTracking();

            if (filters.Categories != null)
                products = products.AsNoTracking().Where(t => filters.Categories.Contains(t.Category));

            //TODO: Сейчас фильтрует по максимально-возможному кол-ву коллорий, можно сделать в промежутке от и до
            if ((filters.CalorieContent.HasValue))
                products = products.AsNoTracking().Where(t => t.CalorieContent <= filters.CalorieContent);

            if ((filters.Handmade.HasValue))
                products = products.AsNoTracking().Where(t => t.Handmade.Value == filters.Handmade.Value);

            //TODO: Сделать фильтр не только по наличию, но и по датам возможной доставки (в наличии с:) 
            if (filters.InStock.HasValue)
                products = products.AsNoTracking().Where(t => t.InStock > 0);

            //TODO: сейчас ищет просто по вхождению части названия производителя, возможно лучше реализовать так: часть названия или id поставщика
            if (!string.IsNullOrEmpty(filters?.Manufacturer))
                products = products.AsNoTracking().Where(t => t.Manufacturer.Contains(filters.Manufacturer));

            if (filters.Sugarless.HasValue)
                products = products.AsNoTracking().Where(t => t.Sugarless.Value == filters.Sugarless.Value);

            //TODO: Сортировка по цене

            //TODO: сортировка по кол-ву в наличии

            var result = new ProductsViewModel(await products.Skip((filters.Page - 1) * filters.Take).Take(filters.Take).ToListAsync(), await products.CountAsync());
            return JsonConvert.SerializeObject(result);
        }

        public async Task<string> GetProduct(string id)
        {
            //TODO: нужно вернуть ошибку, а не null-значение
            if (string.IsNullOrEmpty(id))
                return null;

            var product = await context.Products.SingleOrDefaultAsync(t => t.Id.Equals(id));
            return JsonConvert.SerializeObject(product);
        }

        public async Task<string> AddProduct(Products newProduct)
        {
            if (newProduct != null)
            {
                //TODO: на уровне БД настроить проверку уникальности например по имени продукции
                newProduct.Id = Guid.NewGuid().ToString();
                await context.Products.AddAsync(newProduct);
                await context.SaveChangesAsync();
                return JsonConvert.SerializeObject(newProduct);
            }
            else return null;
        }

        public async Task<string> DeleteProduct(string id)
        {
            var deleteditem = context.Products.FirstOrDefault(t => t.Id.Equals(id));

            if (deleteditem == null)
                return null;

            context.Products.Remove(deleteditem);
            await context.SaveChangesAsync();

            return JsonConvert.SerializeObject(deleteditem);
        }
        //AutoMapper
        public async Task<string> UpdateProduct(ProductViewModel updatedProduct)
        {
            var confg = new MapperConfiguration(cfg => cfg.CreateMap<ProductViewModel, Products>()
            .ForMember(x => x.Id, y => y.UseDestinationValue()));
            var mapper = new Mapper(confg);
            var product = mapper.Map<ProductViewModel, Products>(updatedProduct);
            context.Products.Update(product);
            await context.SaveChangesAsync();
            return JsonConvert.SerializeObject(product);
        }
    }
}

