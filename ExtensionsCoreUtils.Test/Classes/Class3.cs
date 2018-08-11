using ExtensionsCoreUtils.Attributes;
using ExtensionsCoreUtils.Enums;
using ExtensionsCoreUtils.Test.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExtensionsCoreUtils.Test.Classes
{
    [InjectableAttribute(typeof(ITest2), DependencyInjectionTypes.Transient)]
    class Class3 : ITest2
    {
    }
}
