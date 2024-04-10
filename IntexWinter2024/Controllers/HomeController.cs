using IntexWinter2024.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;
using System.Globalization;

namespace IntexWinter2024.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IntexWinter2024Context _context;

        public HomeController(ILogger<HomeController> logger, IntexWinter2024Context context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.ToList(); // Retrieve all products from the database
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

        public IActionResult ProductEdit()
        {
            return View();
        }

        public IActionResult UserEdit()
        {
            return View();
        }

        //public IActionResult Index()
        //{
        //    string filePath = @"C:\Users\samha\Downloads\product_categories - product_categories.csv"; // Update this to your CSV file path

        //    using (TextFieldParser csvParser = new TextFieldParser(filePath))
        //    {
        //        csvParser.TextFieldType = FieldType.Delimited;
        //        csvParser.SetDelimiters(",");
        //        csvParser.HasFieldsEnclosedInQuotes = true;

        //        // Skip the header row
        //        csvParser.ReadLine();

        //        while (!csvParser.EndOfData)
        //        {
        //            string[] fields = csvParser.ReadFields();

        //            //Console.WriteLine($"Adding Order: {fields[0]} \t 1:{fields[1]} \t 1:{fields[2]} \t 1:{fields[3]} \t 1:{fields[4]} \t 1:{fields[5]} \t 1:{fields[6]} \t 1:{fields[7]} \t 1:{fields[8]} \t 1:{fields[9]} \t 1:{fields[10]} \t 1:{fields[11]} \t 1:{fields[12]}");

        //            var order = new ProductCategory
        //            {
        //                // Adjust these to match the order and format of your CSV
        //                ProductId = int.Parse(fields[0]),
        //                CategoryName = fields[1]
        //            };

        //            // Optional: For debugging, printing the added order's info
        //            //Console.WriteLine($"Adding Order Amount: {order.Amount} - Date: {order.Date} - Fraud: {order.Fraud}");

        //            _context.ProductCategories.Add(order);
        //        }

        //        _context.SaveChanges(); // Saves all added orders to the database
        //    }

        //    return View();
        //}


        public IActionResult Privacy()
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
