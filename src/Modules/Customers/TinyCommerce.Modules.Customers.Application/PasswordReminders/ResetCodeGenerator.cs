using System;
using TinyCommerce.Modules.Customers.Domain.PasswordReminders;

namespace TinyCommerce.Modules.Customers.Application.PasswordReminders
{
    public class ResetCodeGenerator : IResetCodeGenerator
    {
        private static string _customCode;

        public string Generate()
        {
            if (!string.IsNullOrEmpty(_customCode))
            {
                return _customCode;
            }

            return Guid.NewGuid().ToString();
        }

        public static void SetCustomCode(string code)
        {
            _customCode = code;
        }

        public static void ResetCustomCode()
        {
            _customCode = null;
        }
    }
}