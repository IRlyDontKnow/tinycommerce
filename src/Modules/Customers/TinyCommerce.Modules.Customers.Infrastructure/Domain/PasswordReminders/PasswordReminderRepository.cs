using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TinyCommerce.Modules.Customers.Domain.PasswordReminders;

namespace TinyCommerce.Modules.Customers.Infrastructure.Domain.PasswordReminders
{
    internal class PasswordReminderRepository : IPasswordReminderRepository
    {
        private readonly CustomersContext _context;

        public PasswordReminderRepository(CustomersContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PasswordReminder passwordReminder)
        {
            await _context.PasswordReminders.AddAsync(passwordReminder);
        }

        public void Remove(PasswordReminder passwordReminder)
        {
            _context.PasswordReminders.Remove(passwordReminder);
        }

        public Task<PasswordReminder> GetByEmailAndCodeAsync(string email, string resetCode)
        {
            return _context.PasswordReminders.FirstOrDefaultAsync(x => x.Email == email && x.Code == resetCode);
        }

        public async Task<PasswordReminder> GetByEmailAsync(string email)
        {
            return await _context.PasswordReminders.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}