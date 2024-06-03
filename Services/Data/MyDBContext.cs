using Microsoft.EntityFrameworkCore;
using ServicesContracts.Model;

namespace Services.Data
{
   
        public class MyDBContext : DbContext
        {
            public MyDBContext(DbContextOptions options) : base(options)
            {
            }

            public DbSet<ProductsModel> Product { get; set; }

        }
    }

