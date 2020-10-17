using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TinyCommerce.Modules.Customers.Application.Contracts;
using TinyCommerce.Modules.Customers.Application.PasswordReminders.ResetPassword;

namespace TinyCommerce.Web.Pages.Security
{
    public class ResetPassword : PageModel
    {
        private readonly ICustomersModule _customersModule;

        public ResetPassword(ICustomersModule customersModule)
        {
            _customersModule = customersModule;
        }

        [BindProperty]
        public ResetPasswordRequest FormData { get; set; }

        [FromQuery(Name = "email")]
        public string Email { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            TempData["LoginInfo"] = "Your password has been successfully changed.";

            await _customersModule.ExecuteCommandAsync(new ResetPasswordCommand(
                Email,
                FormData.Code,
                FormData.Password
            ));

            return RedirectToPage("/Security/Login");
        }
    }

    public class ResetPasswordRequest
    {
        public string Code { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }

    public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.PasswordConfirmation)
                .Equal(x => x.Password);
        }
    }
}