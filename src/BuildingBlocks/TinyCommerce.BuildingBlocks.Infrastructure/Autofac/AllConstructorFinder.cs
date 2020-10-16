using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using Autofac.Core.Activators.Reflection;

namespace TinyCommerce.BuildingBlocks.Infrastructure.Autofac
{
    public class AllConstructorFinder : IConstructorFinder
    {
        public ConstructorInfo[] FindConstructors(Type targetType)
        {
            var result = Cache.GetOrAdd(targetType,
                t => t.GetTypeInfo().DeclaredConstructors.ToArray());

            return result.Length > 0 ? result : throw new NoConstructorsFoundException(targetType);
        }

        private static readonly ConcurrentDictionary<Type, ConstructorInfo[]> Cache =
            new ConcurrentDictionary<Type, ConstructorInfo[]>();
    }
}