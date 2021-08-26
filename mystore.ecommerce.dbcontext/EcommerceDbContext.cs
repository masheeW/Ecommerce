using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using mystore.ecommerce.dbcontext.Models;

#nullable disable

namespace mystore.ecommerce.dbcontext
{
    public partial class EcommerceDbContext : DbContext
    {
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options)
: base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(EcommerceDbContext).Assembly);
        }
    }
}