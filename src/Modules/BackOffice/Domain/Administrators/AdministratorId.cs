using System;
using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.Modules.BackOffice.Domain.Administrators
{
    public class AdministratorId : TypedIdValueBase
    {
        public AdministratorId(Guid value) : base(value)
        {
        }
    }
}
