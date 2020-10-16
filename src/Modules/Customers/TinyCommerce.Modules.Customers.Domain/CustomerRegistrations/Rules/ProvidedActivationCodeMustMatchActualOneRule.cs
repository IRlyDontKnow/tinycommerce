using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.Modules.Customers.Domain.CustomerRegistrations.Rules
{
    public class ProvidedActivationCodeMustMatchActualOneRule : IBusinessRule
    {
        private readonly string _providedActivationCode;
        private readonly string _actualActivationCode;

        public ProvidedActivationCodeMustMatchActualOneRule(string providedActivationCode, string actualActivationCode)
        {
            _providedActivationCode = providedActivationCode;
            _actualActivationCode = actualActivationCode;
        }

        public bool IsBroken()
        {
            return _actualActivationCode != _providedActivationCode;
        }

        public string Message => "Provided activation code must match the actual one.";
    }
}