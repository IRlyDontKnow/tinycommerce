using System.Threading.Tasks;

namespace TinyCommerce.BuildingBlocks.Application.Emails
{
    public interface IEmailSender
    {
        Task SendAsync(EmailMessage message);
    }
}
