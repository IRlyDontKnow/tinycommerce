using System;
using TinyCommerce.Modules.BackOffice.Application.Contracts;

namespace TinyCommerce.Modules.BackOffice.Application.Administrators.GetAdministrator
{
    public class GetAdministratorQuery : IQuery<AdministratorDto>
    {
        public GetAdministratorQuery(Guid administratorId)
        {
            AdministratorId = administratorId;
        }

        public Guid AdministratorId { get; }
    }
}