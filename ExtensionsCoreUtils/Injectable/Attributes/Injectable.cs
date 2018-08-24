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

        /// <summary>
        /// Basic constructor.
        /// </summary>
        /// <param name="instanceOf">Interface</param>
        /// <param name="injectionType">Injection type. By default singlenton</param>
        public InjectableAttribute(Type instanceOf, DependencyInjectionTypes injectionType = DependencyInjectionTypes.Singlenton)
        {
            if (instanceOf is null)
            {
                throw new ArgumentNullException(nameof(instanceOf));
            }
            InstaceOf = instanceOf;
            DependencyType = injectionType;
            //Internal GUID
            Name = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Advance constructor.
        /// </summary>
        /// <param name="instanceOf">Interface</param>
        /// <param name="injectionType">Injection type. By default singlenton.</param>
        /// <param name="name">The reference name.</param>
        public InjectableAttribute(Type instanceOf, DependencyInjectionTypes injectionType, String name)
            :this(instanceOf, injectionType)
        {
            this.Name = name;
        }


        /// <summary>
        /// Reference name
        /// </summary>
        public String Name { get; set; }

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
