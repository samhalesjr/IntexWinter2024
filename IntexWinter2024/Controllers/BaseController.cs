using IntexWinter2024.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace IntexWinter2024.Controllers
{
    public class BaseController : Controller
    {
        private readonly IIntexWinter2024Repository _repo;
        //private readonly UserManager<IdentityUser> _userManager;

        //public BaseController(IntexWinter2024Context context, UserManager<IdentityUser> userManager)
        public BaseController(IIntexWinter2024Repository repo)
        {
            _repo = repo;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            // Ensure User.Identity is authenticated before accessing User
            if (User.Identity.IsAuthenticated)
            {
                var customer = _repo.GetCustomer(User);
                //ViewData["Customer"] = customer;
                //ViewData["CustomerRole"] = customer.Role;
                //var userId = _userManager.GetUserId(User);
                //var customer = _repo.GetCustomerById(userId);  // Assuming there's a method to fetch customer by userId
                if (customer != null)
                {
                    ViewData["Customer"] = customer;
                    ViewData["CustomerRole"] = customer.Role;
                }
            }
        }
    }
}
