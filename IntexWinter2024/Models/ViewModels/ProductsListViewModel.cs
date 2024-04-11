namespace IntexWinter2024.Models.ViewModels
{
    
    public class ProductsListViewModel
    {
        public IQueryable<Product> Products { get; set; }
        public List<string> Categories { get; set; }
        public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();
    }
}