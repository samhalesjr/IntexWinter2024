using IntexWinter2024.Models;
using Microsoft.AspNetCore.Mvc;
using static IntexWinter2024.Models.Cart;

namespace IntexWinter2024.Controllers
{

    public class OrderController : Controller
    {

        private IIntexWinter2024Repository _repo;

        private Cart cart;

        public OrderController(IIntexWinter2024Repository repoService, Cart cartService)
        {
            _repo = repoService;
            cart = cartService;
        }

        public IActionResult Checkout()
        {
            return View(new Order());
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {

            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError
                (
                    "",
                    "Sorry, your cart is empty."
                );
            }

            if (ModelState.IsValid)
            {

                order.Lines = cart.Lines.Select(l => new LineItem
                {
                    ProductId = l.Product.ProductId,
                    Qty = l.Quantity
                }).ToList();

                _repo.SaveOrder(order);
                cart.ClearCart();

                return RedirectToPage("/Completed", new { transactionId = order.TransactionId });
            }
            else
            {
                return View();
            }
        }
    }
}