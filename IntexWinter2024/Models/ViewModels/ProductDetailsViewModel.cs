namespace IntexWinter2024.Models.ViewModels
{
    public class ProductDetailsViewModel
    {
        public ProductCategoryViewModel ProductCategory { get; set; }
        
        public List<Product>? ProductBasedProductRecommendation { get; set; }
    }
}
