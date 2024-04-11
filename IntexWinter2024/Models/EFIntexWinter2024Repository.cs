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

        // Method that saves changes made to a product's information
        public void EditProduct(Product updatedInfo)
        {
            _context.Update(updatedInfo);
            _context.SaveChanges();
        }

        // Method that saves changes made to a customer's information
        public void EditCustomer(Customer updatedInfo)
        {
            _context.Update(updatedInfo);
            _context.SaveChanges();
        }
    }
}
