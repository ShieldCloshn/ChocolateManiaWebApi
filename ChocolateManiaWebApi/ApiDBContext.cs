using ChocolateManiaWebApi.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;

namespace ChocolateManiaWebApi
{
    public class ApiDBContext : DbContext
    {
        public ApiDBContext(DbContextOptions<ApiDBContext> options) : base(options)
        {

        }

        public ApiDBContext()
        {
        }
     
        public DbSet<Products> Products { get; set; }
           
    }
}
