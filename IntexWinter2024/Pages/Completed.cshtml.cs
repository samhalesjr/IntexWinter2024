using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IntexWinter2024.Pages
{
    public class CompletedModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string TransactionId { get; set; }
        public void OnGet()
        {
        }
    }
}
