using ChocolateManiaWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ChocolateMania.Data
{
    public class ApiDBContext : DbContext
    {
        //переоределить метод OnConfiguring 
        public ApiDBContext(DbContextOptions<ApiDBContext> options) : base(options)
        {
        }

        public ApiDBContext()
        {
        }

        public DbSet<Products> Products { get; set; }
        //public DbSet<SoldProducts> SoldProducts { get; set; }


    }
}
