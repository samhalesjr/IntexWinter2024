namespace IntexWinter2024.Models.ViewModels;

public class ProductCategoryViewModel
{
    public Product Products { get; set; }
    public IQueryable<ProductCategory> Categories { get; set; }
}