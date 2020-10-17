using System.Threading.Tasks;
using Quartz;
using TinyCommerce.Modules.Customers.Infrastructure.Configuration;

namespace TinyCommerce.Modules.Customers.Infrastructure.PasswordReminders.CleanupExpiredPasswordReminders
{
    [DisallowConcurrentExecution]
    public class CleanupPasswordRemindersJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await CommandsExecutor.Execute(new CleanupExpiredPasswordRemindersCommand());
        }
    }
}
