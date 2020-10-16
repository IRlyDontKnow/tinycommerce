using System.Threading.Tasks;
using TinyCommerce.Modules.Customers.Application.Contracts;

namespace TinyCommerce.Modules.Customers.Application.Configuration
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync(ICommand command);
    }
}