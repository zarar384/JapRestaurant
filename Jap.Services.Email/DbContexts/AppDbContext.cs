using Jap.Services.EmailAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Jap.Services.EmailAPI.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<EmailLog> EmailLogs { get; set; }
    }
}
