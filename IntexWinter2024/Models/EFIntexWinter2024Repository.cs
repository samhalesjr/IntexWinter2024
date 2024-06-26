﻿using Microsoft.EntityFrameworkCore;
using IntexWinter2024.Models.ViewModels;
﻿using System.Linq;

using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

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
        public IQueryable<ProductCategoryViewModel> ProductCategoryViewModels =>
            from p in _context.Products
            join c in _context.ProductCategories on p.ProductId equals c.ProductId into categories
            select new ProductCategoryViewModel
            {
                Product = p,
                Categories = categories.Select(c => c.CategoryName).ToList()
            }; 
        public IQueryable<Order> Orders => _context.Orders
                                .Include(o => o.Lines)
                                .ThenInclude(l => l.Product);
        public void SaveOrder(Order order)
        {
            _context.AttachRange(order.Lines); //This is l.Line in the book and may need to be that way to not just add the product but the quantity as well
            if (order.TransactionId == 0)
            {
                _context.Orders.Add(order);
            }
            _context.SaveChanges();
        }
        public IQueryable<LineItem> LineItems => _context.LineItems;
        public IQueryable<Role> Roles => _context.Roles;
        public IQueryable<ProductCategory> ProductCategories => _context.ProductCategories;

        // Method that saves changes made to a product's information
        public void EditProduct(Product updatedInfo)
        {
            _context.Update(updatedInfo);
            _context.SaveChanges();
        }

        // Method that deletes a product's information
        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        // Method that saves changes made to a user's information
        public void EditCustomer(Customer updatedInfo)
        {
            _context.Update(updatedInfo);
            _context.SaveChanges();
        }

        // Method that deletes a user's information
        public void DeleteUser(Customer customer)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }

        // Method to get a list of categories for a product. Takes a product Id and returns a list of it's categories
        public List<string> GetCategoriesForProduct(int productId)
        {
            return _context.ProductCategories
                .Where(pc => pc.ProductId == productId)
                .Select(pc => pc.CategoryName)
                .Distinct()
                .ToList();
        }

        // Method to get all distinct categories present in the database
        public List<string> GetAllCategories()
        {
            return _context.ProductCategories
                .Select(pc => pc.CategoryName)
                .Distinct()
                .ToList();
        }

        // Method to get the primary color for a product. Takes a product Id and returns the primary color as a string
        public List<string> GetPrimaryColor(int productId)
        {
            return _context.Products
                .Where(p => p.ProductId == productId)
                .Select(p => p.PrimaryColor)
                .Distinct()
                .OrderBy(x => x)
                .ToList();
        }

        // Method to get all distinct primary colors present in the database
        public List<string> GetAllPrimaryColors()
        {
            return _context.Products
                .Select(p => p.PrimaryColor)
                .Distinct()
                .OrderBy(x => x)
                .ToList();
        }
        public Customer GetCustomer(ClaimsPrincipal user)
        {
            var userId = _userManager.GetUserId(user); // Get the current user's ID
            var customer = _context.Customers.Include(c => c.Role).FirstOrDefault(c => c.UserId == userId);
            return customer;
        }

        public IQueryable<ProductBasedProductRecommendation> ProductBasedProductRecommendations =>
            _context.ProductBasedProductRecommendations;
    }
}
