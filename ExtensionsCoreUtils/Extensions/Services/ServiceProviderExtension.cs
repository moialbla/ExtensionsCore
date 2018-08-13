using ExtensionsCoreUtils.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Service provider extension.
    /// </summary>
    public static class ServiceProviderExtension
    {
        /// <summary>
        /// Extension with key parameter.
        /// </summary>
        /// <param name="ServiceProvider"></param>
        /// <param name="typeService">Type</param>
        /// <param name="key">Key</param>
        /// <returns>The object or default.</returns>
        public static object GetService(this ServiceProvider ServiceProvider, Type typeService, string key)
        {
            IEnumerable<object> typesList = ServiceProvider.GetServices(typeService);
            object objectServiceSearch = InjectionFactory.GetValueOrDefault(key);
            return typesList.Where(x => x.GetType().Equals(objectServiceSearch)).FirstOrDefault();
        }

    }
}
