using System.Threading.Tasks;
using TinyCommerce.Modules.BackOffice.Application.Contracts;

namespace TinyCommerce.Modules.BackOffice.Application.Configuration
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync(ICommand command);
    }
}