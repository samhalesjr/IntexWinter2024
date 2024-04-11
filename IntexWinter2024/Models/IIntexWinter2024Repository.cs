﻿using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query;
using System.Security.Claims;

namespace IntexWinter2024.Models
{
    public interface IIntexWinter2024Repository
    {
        public IQueryable<Customer> Customers { get; }
        public IQueryable<Product> Products { get; }
        public IQueryable<Order> Orders { get; }
        public IQueryable<LineItem> LineItems { get; }
        public IQueryable<Role> Roles { get; }
        public IQueryable<ProductCategory> ProductCategories { get; }
        public void EditProduct(Product updatedInfo);
        public void EditCustomer(Customer updatedInfo);
        public List<string> GetAllCategories();
        public List<string> GetCategoriesForProduct(int productId);
        public Customer GetCustomer(ClaimsPrincipal user);
    }
}
