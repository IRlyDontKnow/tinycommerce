using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TinyCommerce.Modules.BackOffice.Application.Authentication;
using TinyCommerce.Modules.BackOffice.Application.Configuration;
using TinyCommerce.Modules.BackOffice.Domain.Administrators;

namespace TinyCommerce.Modules.BackOffice.Application.Administrators.CreateAdministrator
{
    internal class CreateAdministratorCommandHandler : ICommandHandler<CreateAdministratorCommand>
    {
        private readonly IAdministratorRepository _administratorRepository;
        private readonly IAdministratorCounter _administratorCounter;
        private readonly IPasswordHasher _passwordHasher;

        public CreateAdministratorCommandHandler(
            IAdministratorRepository administratorRepository,
            IAdministratorCounter administratorCounter,
            IPasswordHasher passwordHasher
        )
        {
            _administratorRepository = administratorRepository;
            _administratorCounter = administratorCounter;
            _passwordHasher = passwordHasher;
        }

        public async Task<Unit> Handle(CreateAdministratorCommand command, CancellationToken cancellationToken)
        {
            var hashedPassword = _passwordHasher.HashPassword(command.Password);
            var administrator = Administrator.CreateNew(
                new AdministratorId(command.AdministratorId),
                command.Email,
                hashedPassword,
                command.FirstName,
                command.LastName,
                command.Role,
                _administratorCounter
            );

            await _administratorRepository.AddAsync(administrator);

            return Unit.Value;
        }
    }
}