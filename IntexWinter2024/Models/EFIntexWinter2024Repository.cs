using System.Linq;

using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IntexWinter2024.Models
{
    public class EFIntexWinter2024Repository : IIntexWinter2024Repository
    {
        private IntexWinter2024Context _context;
        private readonly UserManager<IdentityUser> _userManager;
        public EFIntexWinter2024Repository(IntexWinter2024Context context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
        public Customer GetCustomer(ClaimsPrincipal user)
        {
            var userId = _userManager.GetUserId(user); // Get the current user's ID
            var customer = _context.Customers.FirstOrDefault(c => c.UserId == userId);
            return customer;
        }
    }
}
