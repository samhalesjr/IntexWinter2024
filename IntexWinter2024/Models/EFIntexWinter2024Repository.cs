using Microsoft.EntityFrameworkCore;
﻿using System.Linq;

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
        public IQueryable<Order> Orders => _context.Orders
                                .Include(o => o.Lines)
                                .ThenInclude(l => l.Product);
        public void SaveOrder(Order order)
        {
            _context.AttachRange(order.Lines.Select(l => l)); //This is l.Line in the book and may need to be that way to not just add the product but the quantity as well
            if (order.TransactionId == 0)
            {
                _context.Orders.Add(order);
            }
            _context.SaveChanges();
        }
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
