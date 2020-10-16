using System.Reflection;
using TinyCommerce.Modules.Customers.Application.Contracts;

namespace TinyCommerce.Modules.Customers.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        internal static readonly Assembly Application = typeof(ICustomersModule).Assembly;
    }
}