using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.Modules.BackOffice.Domain.Administrators.Rules
{
    public class AdministratorEmailMustBeUniqueRule : IBusinessRule
    {
        private readonly string _email;
        private readonly IAdministratorCounter _counter;

        public AdministratorEmailMustBeUniqueRule(string email, IAdministratorCounter counter)
        {
            _email = email;
            _counter = counter;
        }

        public bool IsBroken()
        {
            return _counter.CountByEmail(_email) > 0;
        }

        public string Message => "Administrator e-mail must be unique rule.";
    }
}