using ExtensionsCoreUtils.Attributes;
using ExtensionsCoreUtils.Enums;
using ExtensionsCoreUtils.Tests.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExtensionsCoreUtils.Tests.Classes
{
    [InjectableAttribute(typeof(ITest2), DependencyInjectionTypes.Scoped, "Test")]
    class Class4 : ITest2
    {
    }
}
