using IntexWinter2024.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError
                (
                    "",
                    "Sorry, your cart is empty."
                );
            }

            if (ModelState.GetFieldValidationState("CustomerId") == ModelValidationState.Valid && ModelState.GetFieldValidationState("Customer") == ModelValidationState.Invalid)
            {
                ModelState.Remove("Customer"); // Remove Customer from model state checking if it's not needed
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