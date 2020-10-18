using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TinyCommerce.Modules.BackOffice.Application.Contracts;

namespace TinyCommerce.Web.Areas.Admin.Pages.Security
{
    public class Login : PageModel
    {
        private readonly IBackOfficeModule _backOfficeModule;

        public Login(IBackOfficeModule backOfficeModule)
        {
            _backOfficeModule = backOfficeModule;
        }

        [BindProperty]
        public LoginAdministratorRequest FormData { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string returnUrl)
        {
            if (!ModelState.IsValid)
                return Page();

            var result = await _backOfficeModule.AuthenticateAdministrator(FormData.Email, FormData.Password);

            if (!result.Authenticated)
            {
                ErrorMessage = result.Error;
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, result.Administrator.Email),
                new Claim("AdministratorId", result.Administrator.Id.ToString()),
                new Claim(ClaimTypes.Role, result.Administrator.Role)
            };
            var claimsIdentity = new ClaimsIdentity(claims, "AdminCookie");

            await HttpContext.SignInAsync("AdminCookie", new ClaimsPrincipal(claimsIdentity));

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToPage("/Index", new
            {
                area = "Admin"
            });
        }
    }

    public class LoginAdministratorRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginAdministratorRequestValidator : AbstractValidator<LoginAdministratorRequest>
    {
        public LoginAdministratorRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}