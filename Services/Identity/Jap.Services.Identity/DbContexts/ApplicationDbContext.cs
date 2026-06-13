using Jap.Services.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Jap.Services.Identity.DbContexts
{
    public class ApplicationDbConext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbConext(DbContextOptions<ApplicationDbConext> options) : base(options)
        {

        }
    }
}
