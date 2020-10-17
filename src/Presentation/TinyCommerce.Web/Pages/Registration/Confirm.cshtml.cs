using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TinyCommerce.Modules.Customers.Application.Contracts;
using TinyCommerce.Modules.Customers.Application.CustomerRegistrations.ConfirmRegistration;
using TinyCommerce.Modules.Customers.Application.CustomerRegistrations.GetCustomerRegistration;

namespace TinyCommerce.Web.Pages.Registration
{
    public class Confirm : PageModel
    {
        private readonly ICustomersModule _customersModule;

        public Confirm(ICustomersModule customersModule)
        {
            _customersModule = customersModule;
        }

        [BindProperty]
        public ConfirmRegistrationRequest FormData { get; set; }

        [FromQuery(Name = "email")]
        public string Email { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            FormData.Email = Email;
            
            if (!ModelState.IsValid)
                return Page();

            var customerRegistration = await _customersModule.ExecuteQueryAsync(
                new GetCustomerRegistrationQuery(email: FormData.Email)
            );

            if (null == customerRegistration)
                return RedirectToPage("/Registration/Index");

            await _customersModule.ExecuteCommandAsync(new ConfirmRegistrationCommand(
                customerRegistration.Id,
                FormData.ActivationCode
            ));

            return RedirectToPage("/Registration/Success");
        }
    }

    public class ConfirmRegistrationRequest
    {
        public string Email { get; set; }

        public string ActivationCode { get; set; }
    }

    public class ConfirmRegistrationRequestValidator : AbstractValidator<ConfirmRegistrationRequest>
    {
        public ConfirmRegistrationRequestValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.ActivationCode).NotEmpty();
        }
    }
}