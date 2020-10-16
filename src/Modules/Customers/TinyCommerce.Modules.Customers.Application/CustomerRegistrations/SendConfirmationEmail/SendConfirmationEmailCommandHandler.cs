using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using TinyCommerce.BuildingBlocks.Application.DataAccess;
using TinyCommerce.BuildingBlocks.Application.Emails;
using TinyCommerce.Modules.Customers.Application.Configuration;

namespace TinyCommerce.Modules.Customers.Application.CustomerRegistrations.SendConfirmationEmail
{
    public class SendConfirmationEmailCommandHandler : ICommandHandler<SendConfirmationEmailCommand>
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly IActivationCodeGenerator _activationCodeGenerator;
        private readonly IEmailSender _emailSender;

        public SendConfirmationEmailCommandHandler(
            IDbConnectionFactory connectionFactory,
            IActivationCodeGenerator activationCodeGenerator,
            IEmailSender emailSender
        )
        {
            _connectionFactory = connectionFactory;
            _activationCodeGenerator = activationCodeGenerator;
            _emailSender = emailSender;
        }

        public async Task<Unit> Handle(SendConfirmationEmailCommand command, CancellationToken cancellationToken)
        {
            var registration = await FetchRegistrationDetails(command.CustomerRegistrationId);

            if (string.IsNullOrEmpty(registration.ActivationCode))
            {
                registration.ActivationCode = await _activationCodeGenerator.GenerateAsync();
                await UpdateActivationCode(command.CustomerRegistrationId, registration.ActivationCode);
            }

            await _emailSender.SendAsync(new EmailMessage(
                registration.Email,
                "TinyCommerce account activation",
                $"Your activation code is: {registration.ActivationCode}"
            ));

            return Unit.Value;
        }

        private async Task<RegistrationDetails> FetchRegistrationDetails(Guid customerRegistrationId)
        {
            var connection = _connectionFactory.GetOpenConnection();
            var sql = $@"
                SELECT
                    email as {nameof(RegistrationDetails.Email)},
                    activation_code as {nameof(RegistrationDetails.ActivationCode)}
                FROM customers.customer_registration 
                WHERE id = @customerRegistrationId
            ";

            return await connection.QuerySingleOrDefaultAsync<RegistrationDetails>(sql, new {customerRegistrationId});
        }

        private async Task UpdateActivationCode(Guid customerRegistrationId, string activationCode)
        {
            var connection = _connectionFactory.GetOpenConnection();
            const string sql = @"
                UPDATE customers.customer_registration 
                SET activation_code = @activationCode
                WHERE id = @customerRegistrationId
            ";

            await connection.ExecuteAsync(sql, new {customerRegistrationId, activationCode});
        }

        [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
        private class RegistrationDetails
        {
            public string Email { get; set; }
            public string ActivationCode { get; set; }
        }
    }
}