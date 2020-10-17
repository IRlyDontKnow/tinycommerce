using System.Threading.Tasks;

namespace TinyCommerce.Modules.Customers.Domain.PasswordReminders
{
    public interface IPasswordReminderRepository
    {
        Task AddAsync(PasswordReminder passwordReminder);

        void Remove(PasswordReminder passwordReminder);

        Task<PasswordReminder> GetByEmailAsync(string email);

        Task<PasswordReminder> GetByEmailAndCodeAsync(string email, string resetCode);
    }
}