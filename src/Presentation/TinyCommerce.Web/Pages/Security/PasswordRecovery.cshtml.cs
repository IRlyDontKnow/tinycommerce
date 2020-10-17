using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TinyCommerce.Modules.Customers.Application.Contracts;
using TinyCommerce.Modules.Customers.Application.PasswordReminders.RequestPasswordReminder;

namespace TinyCommerce.Web.Pages.Security
{
    public class PasswordRecovery : PageModel
    {
        private readonly ICustomersModule _customersModule;

        public PasswordRecovery(ICustomersModule customersModule)
        {
            _customersModule = customersModule;
        }

        [BindProperty]
        public PasswordRecoveryRequest FormData { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            await _customersModule.ExecuteCommandAsync(new RequestPasswordReminderCommand(
                FormData.Email
            ));

            return RedirectToPage("/Security/ResetPassword", new
            {
                email = FormData.Email
            });
        }
    }

    public class PasswordRecoveryRequest
    {
        public string Email { get; set; }
    }

    public class PasswordRecoveryRequestValidator : AbstractValidator<PasswordRecoveryRequest>
    {
        public PasswordRecoveryRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}