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
        
        public class ProductViewModel
        {
            public Product Product { get; set; }
            public List<string> Categories { get; set; }
        }
        
        public class ProductsListViewModel
        {
            public List<ProductViewModel> Products { get; set; }
            public PaginationInfo PaginationInfo { get; set; }
        }

        public IActionResult Browse(int pageNum)
        {
            if (pageNum <= 0) 
            {
                pageNum = 1;
            }

            int pageSize = 5;
            
            // differing from videos, because we split categories into different tables, we'll need to account for that here.
            var products = _repo.Products
                .OrderBy(x => x.ProductId)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            
            var lego = new ProductsListViewModel
            {
                Products = products.Select(p => new ProductViewModel
                {
                    Product = p,
                    Categories = _repo.ProductCategories
                        .Where(pc => pc.ProductId == p.ProductId)
                        .Select(pc => pc.CategoryName)
                        .ToList()
                }).ToList(),

                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = _repo.Products.Count()
                }
            };
            
            return View(lego);
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
