using System.Threading.Tasks;

namespace TinyCommerce.Modules.Customers.Application.CustomerRegistrations
{
    public interface IActivationCodeGenerator
    {
        Task<string> GenerateAsync();
    }
}