using ExtensionsCoreUtils.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExtensionsCoreUtils.Attributes
{
    /// <summary>
    /// Set attribute in class for automatic injection.
    /// <code>
    ///     
    /// </code>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class InjectableAttribute : Attribute
    {
        public InjectableAttribute(Type instanceOf, DependencyInjectionTypes injectionType)
        {   if (instanceOf is null) {
                throw new ArgumentNullException(nameof(instanceOf));
            }
            InstaceOf = instanceOf;
            DependencyType = injectionType;
        }
        /// <summary>
        /// Type of instance.
        /// </summary>
        public Type InstaceOf { get; set; }

        /// <summary>
        /// Type.
        /// </summary>
        public DependencyInjectionTypes DependencyType { get; set; } = DependencyInjectionTypes.Singlenton;
    }
}
