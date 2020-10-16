using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TinyCommerce.Modules.Customers.Application.Contracts;
using TinyCommerce.Modules.Customers.Application.CustomerRegistrations.RegisterNewCustomer;

namespace TinyCommerce.Web.Pages.Registration
{
    public class Index : PageModel
    {
        private readonly ICustomersModule _customersModule;

        public Index(ICustomersModule customersModule)
        {
            _customersModule = customersModule;
        }


        [BindProperty]
        public RegisterCustomerRequest FormData { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _customersModule.ExecuteCommandAsync(new RegisterNewCustomerCommand(
                Guid.NewGuid(),
                FormData.Email,
                FormData.Password,
                FormData.FirstName,
                FormData.LastName
            ));

            return RedirectToPage("/Registration/Confirm", new
            {
                email = FormData.Email
            });
        }

        public class RegisterCustomerRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string PasswordConfirmation { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }
}