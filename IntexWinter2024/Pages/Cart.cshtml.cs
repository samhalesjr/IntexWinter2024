using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IntexWinter2024.Models;
using IntexWinter2024.Infrastructure;

namespace IntexWinter2024.Pages
{
    public class CartModel : PageModel
    {
        public IIntexWinter2024Repository _repo;

        public Cart Cart { get; set; }

        public CartModel(IIntexWinter2024Repository repo, Cart cartService)
        {
            _repo = repo;
            Cart = cartService;
        }

        public string ReturnUrl { get; set; } = "/";

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }
        public IActionResult OnPost(int productId, string returnUrl)
        {
            //Create an instance of a product to pass to the cart
            Product product = _repo.Products
                .FirstOrDefault(x => x.ProductId == productId);
            
            if (product != null)
            {
                //Add the item to the cart, passing the productId and the quantity (set to 1, but could change if we want to choose how many items are added to the cart)
                Cart.AddItem(product, 1);
            }

            return RedirectToPage (new {returnUrl = returnUrl});
        }

        public IActionResult OnPostRemove (int productId, string returnUrl)
        {
            Cart.RemoveLine(Cart.Lines.First(x => x.Product.ProductId == productId).Product);

            return RedirectToPage(new { returnUrl = returnUrl});
        }
    }
}
