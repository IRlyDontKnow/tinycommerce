using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TinyCommerce.Modules.Customers.Application.Contracts;
using TinyCommerce.Modules.Customers.Application.Customers.GetCustomer;

namespace TinyCommerce.Web.ViewComponents
{
    [ViewComponent]
    public class UserMenuViewComponent : ViewComponent
    {
        private readonly ICustomersModule _customersModule;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserMenuViewComponent(ICustomersModule customersModule, IHttpContextAccessor httpContextAccessor)
        {
            _customersModule = customersModule;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var customerId = _httpContextAccessor.HttpContext?.User?.Claims?
                .SingleOrDefault(claim => claim.Type == "CustomerId")?.Value;

            if (string.IsNullOrEmpty(customerId))
            {
                throw new ApplicationException("Failed to resolve customer id.");
            }

            var customer = await _customersModule.ExecuteQueryAsync(new GetCustomerQuery(Guid.Parse(customerId)));

            if (null == customer)
            {
                throw new ApplicationException($"Failed to resolve customer with id '{customerId}'.");
            }

            var viewModel = new UserMenuViewModel
            {
                CustomerFullName = $"{customer.FirstName} {customer.LastName}"
            };

            return View(viewModel);
        }
    }

    public class UserMenuViewModel
    {
        public string CustomerFullName { get; set; }
    }
}