using DotLiquid;
using DotLiquidExtended.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DotLiquidExtended
{
    public static class DotLiquidUtility
    {
        public static void RegisterCustomFilters()
        {
            Template.RegisterFilter(typeof(DotLiquidCustomFilters));
        }
        public static void RegisterSafeTypes(params Type[] types)
        {
            if (types == null || types.Length <= 0) return;

            foreach (var type in types)
            {
                var props = type.GetTypeInfo().GetProperties().Select(x => x.Name).ToArray();
                Template.RegisterSafeType(type, props);
            }
        }
        public static void RegisterSafeTypes(bool withReferencedAssemblies = false)
        {
            var types = new List<Type>();
            types.AddRange(Assembly.GetEntryAssembly()?.GetTypes() ?? throw new InvalidOperationException());

            if (withReferencedAssemblies)
            {
                var refAsm = Assembly.GetEntryAssembly()?.GetReferencedAssemblies();
                if (refAsm != null)
                {
                    foreach (var referencedAssembly in refAsm)
                    {
                        var loadedAssembly = Assembly.Load(referencedAssembly);
                        types.AddRange(loadedAssembly.GetTypes());
                    }
                }
            }

            foreach (var type in types)
            {
                var props = type.GetTypeInfo().GetProperties().Select(x => x.Name).ToArray();
                Template.RegisterSafeType(type, props);
            }
        }
        public static void RegisterSafeTypes(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                var props = type.GetTypeInfo().GetProperties().Select(x => x.Name).ToArray();
                Template.RegisterSafeType(type, props);
            }
        }
        public static void RegisterSafeTypes(List<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    var props = type.GetTypeInfo().GetProperties().Select(x => x.Name).ToArray();
                    Template.RegisterSafeType(type, props);
                }
            }
        }
    }
}
