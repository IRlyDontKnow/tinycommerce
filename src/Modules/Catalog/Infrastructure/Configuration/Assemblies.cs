using System.Reflection;
using TinyCommerce.Modules.Catalog.Application.Contracts;

namespace TinyCommerce.Modules.Catalog.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        internal static readonly Assembly Application = typeof(ICatalogModule).Assembly;
    }
}