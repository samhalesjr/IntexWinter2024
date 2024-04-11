using IntexWinter2024.Models;
using IntexWinter2024.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;
using System.Globalization;


namespace IntexWinter2024.Controllers
{
    public class HomeController : Controller
    {
        private IIntexWinter2024Repository _repo;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IIntexWinter2024Repository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        public IActionResult Index()
        {
            var products = _repo.Products.ToList(); // Retrieve all products from the database
            return View(products); // Pass the products to the view
        }

        public IActionResult AdminLandingPage()
        {
            return View();
        }

        public IActionResult OrderReview()
        {
            var fraudulentOrders = _repo.Orders
                .Where(x => x.Fraud == true)
                .ToList();

            return View(fraudulentOrders);
        }

        public IActionResult ProductEdit()
        {
            var products = _repo.Products
                .ToList();

            return View(products);
        }

        [HttpGet]
        public IActionResult ProductEditPage(int id)
        {
            var productToEdit = _repo.Products
                .SingleOrDefault(x => x.ProductId == id);

            return View(productToEdit);
        }

        [HttpPost]
        public IActionResult ProductEdit(Product updatedInfo)
        {
            _repo.EditProduct(updatedInfo);

            return RedirectToAction("ProductEdit");
        }

        public IActionResult UserEdit()
        {
            var customers = _repo.Customers
                .ToList();

            return View(customers);
        }

        [HttpGet]
        public IActionResult UserEditPage(int id)
        {
            var userToEdit = _repo.Customers
                //.Include(x => x.Role)
                .SingleOrDefault(x => x.CustomerId == id);

            return View(userToEdit);
        }

        [HttpPost]
        public IActionResult UserEdit(Customer updatedInfo)
        {
            _repo.EditCustomer(updatedInfo);

            return RedirectToAction("UserEdit");
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        public IActionResult AboutUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
