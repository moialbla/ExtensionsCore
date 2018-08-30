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
        }
        /// <summary>
        /// String extension, convert the first letter into lower case.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>The string with the first letter in lower case.</returns>
        public static string FirstCharacterToLower(this string str)
        {
            if (String.IsNullOrEmpty(str) || Char.IsLower(str, 0))
            {
                return str;
            }

            return Char.ToLowerInvariant(str[0]) + str.Substring(1);
        }
    }
}
