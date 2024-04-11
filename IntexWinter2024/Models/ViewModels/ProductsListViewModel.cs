namespace IntexWinter2024.Models.ViewModels
{
    
    public class ProductsListViewModel
    {
        public List<ProductViewModel> Products { get; set; }
        public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();
    }
}