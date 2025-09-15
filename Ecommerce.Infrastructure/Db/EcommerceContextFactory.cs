using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Db
{
    public class EcommerceContextFactory : IDesignTimeDbContextFactory<EcommerceContext>
    {
        public EcommerceContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EcommerceContext>();

            optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress; Initial Catalog=Ecommerce;Integrated Security=True;TrustServerCertificate=True;");

            return new EcommerceContext(optionsBuilder.Options);
        }
    }
}