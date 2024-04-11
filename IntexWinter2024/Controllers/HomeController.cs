using IntexWinter2024.Components;
using IntexWinter2024.Models;
using IntexWinter2024.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;
using System.Globalization;
using System.Linq;


namespace IntexWinter2024.Controllers
{
    public class HomeController : BaseController
    {
        private IIntexWinter2024Repository _repo;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IIntexWinter2024Repository repo)
            : base(repo)
        {
            _logger = logger;
            _repo = repo;
        }

        public IActionResult Index()
        {
            var products = _repo.Products.ToList(); // Retrieve all products from the database
            return View(products); // Pass the products to the view
        }

        public IActionResult Browse(int pageNum, string productCategory)
        {
            if (pageNum <= 0)
            {
                pageNum = 1;
            }

            int pageSize = 5;

            // Here we're differing from the videos. We need to because the differing tables.
            // Query products from the repository
            var productsQuery = _repo.Products;

            // Filter products based on the selected category
            if (!string.IsNullOrEmpty(productCategory))
            {
                productsQuery = productsQuery
                    .Where(p => _repo.ProductCategories
                        .Any(pc => pc.ProductId == p.ProductId && pc.CategoryName == productCategory));
            }

            // Order the products by ProductId
            productsQuery = productsQuery.OrderBy(p => p.ProductId);

            // Paginate the filtered and ordered products
            var products = productsQuery
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var productViewModels = new List<ProductCategoryViewModel>();

            foreach (var product in products)
            {
                var categories = _repo.GetCategoriesForProduct(product.ProductId);

                var productViewModel = new ProductCategoryViewModel
                {
                    Product = product,
                    Categories = categories
                };

                productViewModels.Add(productViewModel);
            }

            // Create the view model
            var lego = new ProductsListViewModel
            { 
                Categories = _repo.GetAllCategories(),

                PaginationInfo = new PaginationInfo()
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = productsQuery.Count()// Count the total filtered products
                },

                ProductCategoryViewModels = productViewModels
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
