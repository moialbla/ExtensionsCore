using ExtensionsCoreUtils.Attributes;
using ExtensionsCoreUtils.Enums;
using ExtensionsCoreUtils.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Static class for extension the Microsoft.Extensions.DependencyInjection namespace.
    /// </summary>
    public static class IServiceCollectionExtension
    {
        /// <summary>
        /// Extension from Microsoft.Extensions.DependencyInjection. 
        /// This method auto scan all classes with the attribute,<remarks>InjectableAttribute</remarks> and put automatically in the container services.
        /// </summary>
        /// <example>
        /// <code>
        ///   public interface ITest1 {}
        ///   
        ///   [InjectableAttribute(typeof(ITest1))]
        ///   public class Class1 : ITest1{ }
        ///   
        ///   [InjectableAttribute(typeof(ITest1), DependencyInjectionTypes.Scoped)]
        ///   public class Class1 : ITest1{ }
        ///   
        ///   [InjectableAttribute(typeof(ITest1), DependencyInjectionTypes.Scoped, "myClass1")]
        ///   public class Class1 : ITest1{ }
        ///   </code>
        /// </example>
        /// <param name="services"></param>
        /// <param name="assemblies">Assemblies list by comma, or GetEntryAssembly by default</param>
        /// <returns>ISericeCollection to continue the configuration.</returns>
        public static IServiceCollection ScanInjections(this IServiceCollection services, params string[] assemblies)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            InjectionFactory.Clear();

            IEnumerable<Type> types = GetListOfTypes(assemblies);

            foreach (Type zclass in types)
            {
                foreach (var injectable in zclass.GetTypeInfo().GetCustomAttributes<InjectableAttribute>())
                {
                    foreach (var zinterface in zclass.GetTypeInfo().ImplementedInterfaces)
                    {
                        AddInjection(services, injectable.DependencyType, zinterface, zclass);
                        AddInternalInjection(injectable.Name, zclass);
                    }
                }
            }
            return services;
        }

        private static IEnumerable<Type> GetListOfTypes(string[] assemblies)
        {

            Type[] types;

            if (!(assemblies is null) && assemblies.Any())
            {
                types = AppDomain.CurrentDomain
                    .GetAssemblies()
                    .Where(assembly => assemblies.Contains(assembly.GetName().Name))
                    .SelectMany(assembly => assembly.GetTypes()).ToArray();
            }
            else
            {
                types = Assembly.GetEntryAssembly().GetTypes();
            }
            return from zclass in types
                   where zclass.GetTypeInfo().IsClass
                   select zclass;
        }

        private static void AddInjection(IServiceCollection services,
            DependencyInjectionTypes type,
            Type zinterface,
            Type zclass)
        {

            if (type == DependencyInjectionTypes.Singlenton)
            {
                services.AddSingleton(zinterface, zclass);
            }
            else if (type == DependencyInjectionTypes.Scoped)
            {
                services.AddScoped(zinterface, zclass);
            }
            else if (type == DependencyInjectionTypes.Transient)
            {
                services.AddTransient(zinterface, zclass);
            }
        }

        private static void AddInternalInjection(string key, Type zclass)
        {
            InjectionFactory.Add(key, zclass);
        }
    }
}

