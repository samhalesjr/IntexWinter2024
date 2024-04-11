namespace IntexWinter2024.Models.ViewModels
{
    public class ProductViewModel
    {
        public IQueryable<Product> Product { get; set; }
        public List<string> Categories { get; set; }
    }
}
