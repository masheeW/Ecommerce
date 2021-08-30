using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using mystore.ecommerce.dbcontext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mystore.ecommerce.dbcontext
{
    public class EcommerceIdentityContext : IdentityDbContext<StoreUser>
    {
        public EcommerceIdentityContext(DbContextOptions<EcommerceIdentityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(EcommerceIdentityContext).Assembly);
        }
    }
}
