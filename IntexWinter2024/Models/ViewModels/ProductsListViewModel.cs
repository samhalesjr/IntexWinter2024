namespace IntexWinter2024.Models.ViewModels
{
    
    public class ProductsListViewModel
    {
        public List<ProductCategoryViewModel> Products { get; set; }
        public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();
    }
}