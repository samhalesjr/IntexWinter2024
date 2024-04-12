namespace IntexWinter2024.Models.ViewModels
{
    
    public class ProductsListViewModel
    {
        public List<ProductCategoryViewModel> ProductCategoryViewModels { get; set; }
        public List<string> PrimaryColors { get; set; }
        public List<string> Categories { get; set; }
        public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();
    }
}