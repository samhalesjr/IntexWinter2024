using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IntexWinter2024.Models;

namespace IntexWinter2024.Pages
{
    public class CartModel : PageModel
    {
        public IIntexWinter2024Repository _repo;

        public CartModel(IIntexWinter2024Repository repo)
        {
            _repo = repo;
        }

        public Cart? Cart { get; set; }

        public void OnGet()
        {
        }
        public void OnPost(int productId)
        {
            //Create an instance of a product to pass to the cart
            Product product = _repo.Products
                .FirstOrDefault(x => x.ProductId == productId);

            Cart = new Cart();

            //Add the item to the cart, passing the productId and the quantity (set to 1, but could change if we want to choose how many items are added to the cart)
            Cart.AddItem(product, 1);
        }
    }
}
