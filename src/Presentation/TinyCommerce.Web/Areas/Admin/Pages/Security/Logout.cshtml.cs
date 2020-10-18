using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TinyCommerce.Web.Areas.Admin.Pages.Security
{
    public class Logout : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            await HttpContext.SignOutAsync("AdminCookie");
            return RedirectToPage("/Index", new
            {
                area = "Admin"
            });
        }
    }
}