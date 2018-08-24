using System;
using System.Collections.Generic;
using System.Text;

namespace ExtensionsCoreUtils.Attributes
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class DomainValidationNameAttribute : Attribute
    {

        public DomainValidationNameAttribute() { }

        public DomainValidationNameAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// The Name
        /// </summary>
        public String Name { get; set; }
    }
}
