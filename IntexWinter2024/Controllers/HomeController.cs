using IntexWinter2024.Models;
using IntexWinter2024.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
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
            return View();
        }

        public IActionResult ProductDetails(int productId)
        {
            var productToView = _repo.Products
                .SingleOrDefault(x => x.ProductId == productId);
            
            if (productToView == null)
            {
                return NotFound();
            }
            
            return View(productToView);
        }

        public IActionResult ProductEdit()
        {
            return View();
        }

        public IActionResult UserEdit()
        {
            return View();
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
