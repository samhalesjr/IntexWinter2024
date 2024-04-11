using IntexWinter2024.Models;
using Microsoft.AspNetCore.Mvc;
using static IntexWinter2024.Models.Cart;

namespace IntexWinter2024.Controllers
{

    public class OrderController : BaseController
    {

        private IIntexWinter2024Repository _repo;

        private Cart cart;

        public OrderController(IIntexWinter2024Repository repoService, Cart cartService)
            : base(repoService)
        {
            _repo = repoService;
            cart = cartService;
        }

        public IActionResult Checkout()
        {
            var customerId = ViewData["CustomerId"];
            return View(new Order());
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            //order.Date = DateTime.Now;
            //order.DayOfWeek = order.Date.ToString("ddd");


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
                //order.Lines = cart.Lines.ToArray(); //The book does it this way
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