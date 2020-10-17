using System;
using TinyCommerce.BuildingBlocks.Domain;

namespace TinyCommerce.Modules.Customers.Domain.Customers.Rules
{
    public class ProvidedPasswordMustMatchCurrentOneRule : IBusinessRule
    {
        private readonly string _currentPassword;
        private readonly Func<string, bool> _verifyHashedPassword;

        public ProvidedPasswordMustMatchCurrentOneRule(string currentPassword, Func<string, bool> verifyHashedPassword)
        {
            _currentPassword = currentPassword;
            _verifyHashedPassword = verifyHashedPassword;
        }

        public bool IsBroken()
        {
            return !_verifyHashedPassword(_currentPassword);
        }

        public string Message => "Provided password must be the same as the current one.";
    }
}