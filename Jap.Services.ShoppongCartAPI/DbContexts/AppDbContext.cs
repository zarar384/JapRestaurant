using Jap.Services.ShoppingCartAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Jap.Services.ShoppingCartAPI.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }
        public DbSet<CartHeader> CartHeaders { get; set; }

    }
}
