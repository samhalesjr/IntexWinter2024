namespace IntexWinter2024.Models.ViewModels
{
    
    public class ProductsListViewModel
    {
        public IQueryable<ProductCategoryViewModel> ProductCategoryViewModels { get; set; }
        //public IQueryable<Product> Products { get; set; }
        //public IQueryable<ProductCategory> Categories { get; set; }
        public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();
    }
}