using System.Threading.Tasks;
using Serilog;
using TinyCommerce.BuildingBlocks.Application.Emails;

namespace TinyCommerce.BuildingBlocks.Infrastructure.Emails
{
    public class LogEmailSender : IEmailSender
    {
        private readonly ILogger _logger;

        public LogEmailSender(ILogger logger)
        {
            _logger = logger;
        }

        public Task SendAsync(EmailMessage message)
        {
            _logger.Information(
                "Send email. To={Email}, Subject={Subject}, Content={Content}",
                message.Email,
                message.Subject,
                message.Content
            );

            return Task.CompletedTask;
        }
    }
}