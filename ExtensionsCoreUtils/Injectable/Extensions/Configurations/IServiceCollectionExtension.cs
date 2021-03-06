﻿using ExtensionsCoreUtils.Attributes;
using ExtensionsCoreUtils.Enums;
using ExtensionsCoreUtils.Factories;
using ExtensionsCoreUtils.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Debug = System.Diagnostics.Debug;
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

            foreach (Type zclass in CoreUtils.GetListOfTypes(assemblies))
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

