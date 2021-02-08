using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using nexo.Models;

namespace Nexo.data
{
    public class AppDbContext: IdentityDbContext<Client>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public AppDbContext()
        {
            
        }

        public DbSet<Product> Products { get; set; }

       
        
    }

    
}