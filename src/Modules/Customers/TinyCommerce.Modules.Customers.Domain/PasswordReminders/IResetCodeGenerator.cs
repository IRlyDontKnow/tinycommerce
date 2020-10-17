namespace TinyCommerce.Modules.Customers.Domain.PasswordReminders
{
    public interface IResetCodeGenerator
    {
        string Generate();
    }
}
