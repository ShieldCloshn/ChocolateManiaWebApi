using AutoMapper;
using ChocolateMania.Data;
using ChocolateMania.Models.ShopModels;
using ChocolateMania.Models.ShopViewModels;
using ChocolateManiaWebApi.Filters;
using ChocolateManiaWebApi.ViewModels;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ProductsViewModel> GetProducts(GetProductsFilter filters)
        {
            var products = context.Products.AsNoTracking();

            if (filters.Categories != null)
            {
                products = products.AsNoTracking().Where(t => filters.Categories.Contains(t.CategoryId));
            }

            //TODO: Сейчас фильтрует по максимально-возможному кол-ву коллорий, можно сделать в промежутке от и до
            if (filters.CalorieContent.HasValue)
            {
                products = products.AsNoTracking().Where(t => t.CalorieContent <= filters.CalorieContent);
            }

            if (filters.Handmade.HasValue)
            {
                products = products.AsNoTracking().Where(t => t.Handmade.Value == filters.Handmade.Value);
            }

            //TODO: Сделать фильтр не только по наличию, но и по датам возможной доставки (в наличии с:) 
            if (filters.InStock.HasValue)
            {
                products = products.AsNoTracking().Where(t => t.InStock > 0);
            }

            //TODO: сейчас ищет просто по вхождению части названия производителя, возможно лучше реализовать так: часть названия или id поставщика
            if (!string.IsNullOrEmpty(filters?.Manufacturer))
            {
                products = products.AsNoTracking().Where(t => t.Manufacturer.Contains(filters.Manufacturer));
            }

            if (filters.Sugarless.HasValue)
            {
                products = products.AsNoTracking().Where(t => t.Sugarless.Value == filters.Sugarless.Value);
            }

            //TODO: Сортировка по цене

            //TODO: сортировка по кол-ву в наличии

            var allProducts = await products.Skip((filters.Page - 1) * filters.Take).Take(filters.Take).ToListAsync();

            var result = new ProductsViewModel(allProducts, await products.CountAsync());
            return result;
        }

        public async Task<ProductViewModel> GetProduct(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException();
            }

            var product = await context.Products.SingleOrDefaultAsync(t => t.Id.Equals(id));

            var confg = new MapperConfiguration(cfg => cfg.CreateMap<Products, ProductViewModel>());
            var mapper = new Mapper(confg);
            var productView = mapper.Map<Products, ProductViewModel>(product);

            return productView;
        }

        public async Task<ProductViewModel> AddProduct(ProductViewModel newProduct)
        {
            if (newProduct != null)
            {
                var confg = new MapperConfiguration(cfg => cfg.CreateMap<ProductViewModel, Products>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString())));

                var mapper = new Mapper(confg);
                var product = mapper.Map<ProductViewModel, Products>(newProduct);

                //TODO: на уровне БД настроить проверку уникальности например по имени продукции
                await context.Products.AddAsync(product);
                await context.SaveChangesAsync();
                return newProduct;
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var deleteditem = context.Products.FirstOrDefault(t => t.Id.Equals(id));

            if (deleteditem == null)
            {
                throw new DbUpdateConcurrencyException();
            }

            context.Products.Remove(deleteditem);
            await context.SaveChangesAsync();

            return true;
        }

        //Возможно лучше вернуть объект
        public async Task<bool> UpdateProduct(ProductViewModel updatedProduct)
        {
            var confg = new MapperConfiguration(cfg => cfg.CreateMap<ProductViewModel, Products>());
            var mapper = new Mapper(confg);
            var product = mapper.Map<ProductViewModel, Products>(updatedProduct);

            try
            {
                context.Products.Update(product);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw new DbUpdateConcurrencyException();
            }
        }
    }
}

