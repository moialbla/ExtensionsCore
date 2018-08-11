using ExtensionsCoreUtils.Attributes;
using ExtensionsCoreUtils.Enums;
using ExtensionsCoreUtils.Test.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExtensionsCoreUtils.Test.Classes
{
    [InjectableAttribute(typeof(ITest1), DependencyInjectionTypes.Scoped)]
    class Class1 : ITest1
    {
    }
}
