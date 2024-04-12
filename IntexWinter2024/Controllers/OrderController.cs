using IntexWinter2024.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static IntexWinter2024.Models.Cart;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace IntexWinter2024.Controllers
{

    public class OrderController : BaseController
    {

        private IIntexWinter2024Repository _repo;
        private readonly InferenceSession _session;

        private Cart cart;

        public OrderController(IIntexWinter2024Repository repoService, Cart cartService)
            : base(repoService)
        {
            _repo = repoService;
            cart = cartService;
            
            // initializing the InferenceSession here.
            try
            {
                _session = new InferenceSession("decision_tree_model.onnx");
                // _logger.LogInformation("ONNX model loaded successfully.");
            }
            catch (Exception ex)
            {
                // _logger.LogError($"Error loading the ONNX model: {ex.Message}");
            }
        }

        public IActionResult Checkout()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");
            var customerId = ViewData["CustomerId"];
            return View(new Order());
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login","Account");

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
                
                // paste in everything
                //create variable to add to the list
                 var amount = (float)0;
                 var time = (float)0;
                 var day_of_week_Mon = 0;
                 var day_of_week_Tue = 0;
                 var day_of_week_Wed = 0;
                 var day_of_week_Thu = 0;
                 var day_of_week_Sat = 0;
                 var day_of_week_Sun = 0;
                 var entry_mode_PIN = 0; // always 0
                 var entry_mode_Tap = 0; // always 0
                 var type_of_transaction_Online = 1; // always 1
                 var type_of_transaction_POS = 1; // always 1
                 var shipping_address_India = 0;
                 var shipping_address_Russia = 0;
                 var shipping_address_USA = 0;
                 var shipping_address_UnitedKingdom = 0;
                 var country_of_transaction_India = 0;
                 var country_of_transaction_Russia = 0;
                 var country_of_transaction_USA = 0;
                 var country_of_transaction_UnitedKingdom = 0;
                 var bank_HSBC = 0;
                 var bank_Halifax = 0;
                 var bank_Lloyds = 0;
                 var bank_Metro = 0;
                 var bank_Monzo = 0;
                 var bank_RBS = 0;
                 var type_of_card_Visa = 0;
     
                 try
                 {
                     // figure out the amount
                     if (order.Amount != null)
                     {
                         amount = (float)order.Amount;
                     }
     
                     // figure out the time
                     if (order.Date.Hour != null)
                     {
                         time = (float)order.Date.Hour;
                     }
     
                     // figure out days of the week
                     if (order.DayOfWeek == "Monday")
                     {
                         day_of_week_Mon = 1;
                     }
                     else if (order.DayOfWeek == "Tuesday")
                     {
                         day_of_week_Tue = 1;
                     }
                     else if (order.DayOfWeek == "Wednesday")
                     {
                         day_of_week_Wed = 1;
                     }
                     else if (order.DayOfWeek == "Thursday")
                     {
                         day_of_week_Thu = 1;
                     }
                     else if (order.DayOfWeek == "Saturday")
                     {
                         day_of_week_Sat = 1;
                     }
                     else if (order.DayOfWeek == "Sunday")
                     {
                         day_of_week_Sun = 1;
                     }
     
                     // figure out entry mode
                     if (order.EntryMode == "PIN")
                     {
                         entry_mode_PIN = 1;
                     }
                     else if (order.EntryMode == "Tap")
                     {
                         entry_mode_Tap = 1;
                     }
     
                     // figure out the type of transaction
                     if (order.TypeOfTransaction == "Online")
                     {
                         type_of_transaction_Online = 1;
                     }
                     else if (order.TypeOfTransaction == "POS")
                     {
                         type_of_transaction_POS = 1;
                     }
     
                     // figure out the shipping address
                     if (order.ShippingAddress == "India")
                     {
                         shipping_address_India = 1;
                     }
                     else if (order.ShippingAddress == "Russia")
                     {
                         shipping_address_Russia = 1;
                     }
                     else if (order.ShippingAddress == "USA")
                     {
                         shipping_address_USA = 1;
                     }
                     else if (order.ShippingAddress == "UnitedKingdom")
                     {
                         shipping_address_UnitedKingdom = 1;
                     }
     
                     // figure out the country of transaction
                     if (order.CountryOfTransaction == "India")
                     {
                         country_of_transaction_India = 1;
                     }
                     else if (order.CountryOfTransaction == "Russia")
                     {
                         country_of_transaction_Russia = 1;
                     }
                     else if (order.CountryOfTransaction == "USA")
                     {
                         country_of_transaction_USA = 1;
                     }
                     else if (order.CountryOfTransaction == "UnitedKingdom")
                     {
                         country_of_transaction_UnitedKingdom = 1;
                     }
     
                     // and... figure out the bank
                     if (order.Bank == "HSBC")
                     {
                         bank_HSBC = 1;
                     }
                     else if (order.Bank == "Halifax")
                     {
                         bank_Halifax = 1;
                     }
                     else if (order.Bank == "Lloyds")
                     {
                         bank_Lloyds = 1;
                     }
                     else if (order.Bank == "Metro")
                     {
                         bank_Metro = 1;
                     }
                     else if (order.Bank == "Monzo")
                     {
                         bank_Monzo = 1;
                     }
                     else if (order.Bank == "RBS")
                     {
                         bank_RBS = 1;
                     }
     
                     // assign type of card
                     if (order.TypeOfCard == "VISA")
                     {
                         type_of_card_Visa = 1;
                     }
     
                     // make a big list to process order to match the feature order from onnx model
                     var input = new List<float>
                     {
                         amount,
                         time,
                         day_of_week_Mon,
                         day_of_week_Tue,
                         day_of_week_Wed,
                         day_of_week_Thu,
                         day_of_week_Sat,
                         day_of_week_Sun,
                         entry_mode_PIN,
                         entry_mode_Tap,
                         type_of_transaction_Online,
                         type_of_transaction_POS,
                         shipping_address_India,
                         shipping_address_Russia,
                         shipping_address_USA,
                         shipping_address_UnitedKingdom,
                         country_of_transaction_India,
                         country_of_transaction_Russia,
                         country_of_transaction_USA,
                         country_of_transaction_UnitedKingdom,
                         bank_HSBC,
                         bank_Halifax,
                         bank_Lloyds,
                         bank_Metro,
                         bank_Monzo,
                         bank_RBS,
                         type_of_card_Visa
                     };
     
                     var inputTensor = new DenseTensor<float>(input.ToArray(), new[] { 1, input.Count });
     
                     var inputs = new List<NamedOnnxValue>
                     {
                         NamedOnnxValue.CreateFromTensor("float_input", inputTensor)
                     };
                    
                     using (var results = _session.Run(inputs)) // makes the prediction with the inputs from the form (i.e. class_type 1-7)
                     {
                         var prediction = results.FirstOrDefault(item => item.Name == "output_label")?.AsTensor<long>().ToArray();
                         if (prediction != null && prediction.Length > 0)
                         {
                            // then it is a fraud
                            order.Flagged = true;
                            _repo.SaveOrder(order);
                            cart.ClearCart();

                            return View("Fraudulent");
                         }
                         else
                         {
                            // then it's legit
                            order.Flagged = false;
                         }
                     }
                     
                     
                 }
                 catch (Exception ex)
                 {
                     // _logger.LogError($"Error during prediction: {ex.Message}");
                     ViewBag.Prediction = "Error during prediction.";
                 }

                
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