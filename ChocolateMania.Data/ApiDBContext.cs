using ChocolateMania.Models.ShopModels;
using Microsoft.EntityFrameworkCore;

namespace ChocolateMania.Data
{
    public class ApiDBContext : DbContext
    {
        //TODO: Переопределить OnConfig и реализовать проверку корректности строки подключения к БД 
        public ApiDBContext(DbContextOptions<ApiDBContext> options) : base(options)
        {
        }

        public ApiDBContext()
        {
        }

        public DbSet<Products> Products { get; set; }
        public DbSet<SoldProducts> SoldProducts { get; set; }
        public DbSet<Categories> Categories { get; set; }
    }
}
