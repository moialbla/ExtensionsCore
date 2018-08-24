using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ExtensionsCoreUtils.Utils
{
    /// <summary>
    /// Core functions utils.
    /// </summary>
    internal static class CoreUtils
    {

        static Func<Type, bool> whereDummy = (t) => true;

        /// <summary>
        /// Get all types by assemblies.
        /// </summary>
        /// <param name="assemblies">The list of assemblies</param>
        /// <returns>Type list</returns>
        internal static IEnumerable<Type> GetListOfTypes(string[] assemblies, Func<Type, bool> where = null)
        {

            Type[] types;

            if (!(assemblies is null) && assemblies.Any())
            {
                return types = AppDomain.CurrentDomain
                    .GetAssemblies()
                    .Where(assembly => assemblies.Contains(assembly.GetName().Name))
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(zclass => zclass.GetTypeInfo().IsClass)
                    .Where(where ?? whereDummy)
                    .ToArray();
            }
            else
            {
                return  types = Assembly.GetEntryAssembly().GetTypes();
            }
            //return from zclass in types
            //       where zclass.GetTypeInfo().IsClass
            //       && zclass.GetCustomAttribute(typeof())
            //       select zclass;
        }
    }
}
