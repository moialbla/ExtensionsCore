
using System;
using System.Collections.Generic;
using System.Text;

namespace ExtensionsCoreUtils.Enums
{
    /// <summary>
    /// Dependency injection parameters.
    /// </summary>
    public enum DependencyInjectionTypes
    {

        /// <summary>
        /// "Scoped lifetime services are created once per request"
        /// </summary>
        Scoped ,
        /// <summary>
        ///  "Singleton lifetime services are created the first time they're requested (or when ConfigureServices is run and an instance is specified with the service registration)."
        /// </summary>
        Singlenton ,
        /// <summary>
        ///  "Transient lifetime services are created each time they're requested. This lifetime works best for lightweight, stateless services."
        /// </summary>
        Transient
    }
}
