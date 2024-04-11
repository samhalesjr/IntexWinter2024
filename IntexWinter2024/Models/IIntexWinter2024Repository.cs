using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace IntexWinter2024.Models
{
    public interface IIntexWinter2024Repository
    {
        public IQueryable<Customer> Customers { get; }
        public IQueryable<Product> Products { get; }
        public IQueryable<Order> Orders { get; }
        void SaveOrder(Order order);
        public IQueryable<LineItem> LineItems { get; }
        public IQueryable<Role> Roles { get; }
        public IQueryable<ProductCategory> ProductCategories { get; }
        public List<string> GetAllCategories();
        public List<string> GetCategoriesForProduct(int productId);
    }
}
