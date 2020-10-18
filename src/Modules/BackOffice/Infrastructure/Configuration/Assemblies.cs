using System.Reflection;
using TinyCommerce.Modules.BackOffice.Application.Contracts;

namespace TinyCommerce.Modules.BackOffice.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        internal static readonly Assembly Application = typeof(IBackOfficeModule).Assembly;
    }
}