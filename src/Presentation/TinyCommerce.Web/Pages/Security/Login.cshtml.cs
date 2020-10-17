using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TinyCommerce.Modules.Customers.Application.Contracts;

namespace TinyCommerce.Web.Pages.Security
{
    public class Login : PageModel
    {
        private readonly ICustomersModule _customersModule;

        public Login(ICustomersModule customersModule)
        {
            _customersModule = customersModule;
        }

        [BindProperty]
        public CustomerLoginRequest FormData { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Account/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPost(string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _customersModule.Authenticate(FormData.Email, FormData.Password);

            if (!result.Authenticated)
            {
                ErrorMessage = result.Error;
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, result.Customer.Email),
                new Claim("CustomerId", result.Customer.Id.ToString()),
                new Claim(ClaimTypes.Role, "Customer")
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)
            );

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToPage("/Index");
        }
    }

    public class CustomerLoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class CustomerLoginRequestValidator : AbstractValidator<CustomerLoginRequest>
    {
        public CustomerLoginRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}