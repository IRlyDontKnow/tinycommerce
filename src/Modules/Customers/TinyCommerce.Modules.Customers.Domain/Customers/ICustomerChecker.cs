namespace TinyCommerce.Modules.Customers.Domain.Customers
{
    public interface ICustomerChecker
    {
        bool IsCustomerEmailInUse(string email);
    }
}