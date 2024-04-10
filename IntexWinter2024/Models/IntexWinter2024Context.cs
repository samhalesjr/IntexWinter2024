using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IntexWinter2024.Models
{
    public class IntexWinter2024Context : IdentityDbContext
    {
        public IntexWinter2024Context()
        {
        }
        public IntexWinter2024Context(DbContextOptions<IntexWinter2024Context> options) 
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<LineItem> LineItems { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductBasedProductRecommendation> ProductBasedProductRecommendations { get; set; }

    }
}
