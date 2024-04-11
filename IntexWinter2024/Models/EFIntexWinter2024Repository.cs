using System.Linq;

namespace IntexWinter2024.Models
{
    public class EFIntexWinter2024Repository : IIntexWinter2024Repository
    {
        private IntexWinter2024Context _context;
        public EFIntexWinter2024Repository(IntexWinter2024Context context)
            {
                _context = context;
            }
        public IQueryable<Customer> Customers => _context.Customers;
        public IQueryable<Product> Products => _context.Products;
        public IQueryable<Order> Orders => _context.Orders;
        public IQueryable<LineItem> LineItems => _context.LineItems;
        public IQueryable<Role> Roles => _context.Roles;
        public IQueryable<ProductCategory> ProductCategories => _context.ProductCategories;

        public List<string> GetCategoriesForProduct(int productId)
        {
            return _context.ProductCategories
                .Where(pc => pc.ProductId == productId)
                .Select(pc => pc.CategoryName)
                .Distinct()
                .ToList();
        }

        public List<string> GetAllCategories()
        {
            return _context.ProductCategories
                .Select(pc => pc.CategoryName)
                .Distinct()
                .ToList();
        }
    }
}
