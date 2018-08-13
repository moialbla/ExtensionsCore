using ExtensionsCoreUtils.Attributes;
using ExtensionsCoreUtils.Tests.Classes;
using ExtensionsCoreUtils.Tests.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System;
using System.Collections.Generic;
using Xunit;
using ExtensionsCoreUtils.Enums;

namespace ExtensionsCoreUtils.Tests
{
    public class InjectableTestx
    {
        private const string Expected = "Value cannot be null.\r\nParameter name: instanceOf";
        private const string Expected1 = "Value cannot be null.\r\nParameter name: services";   

        [Fact]
        public void Test_Dependency_Injection_Start()
        {
            IServiceCollection service = new ServiceCollection().ScanInjections("ExtensionsCoreUtils.Tests", "ExtensionsCoreUtils.Tests.other");
            Assert.Equal(4, service.Count);
            service = new ServiceCollection().ScanInjections();
            Assert.Equal(0, service.Count);
            service = new ServiceCollection().ScanInjections("ExtensionsCoreUtils.Tests.other");
            Assert.Equal(0, service.Count);
            service = new ServiceCollection().ScanInjections("ExtensionsCoreUtils.Tests");
            Assert.Equal(4, service.Count);
            ServiceProvider serviceProvider = service.BuildServiceProvider();
            var type = serviceProvider.GetService(typeof(ITest1));
            Assert.True(type is Class1);
            IEnumerable<object> typesList = serviceProvider.GetServices(typeof(ITest2));
            Assert.True(typesList.Count() == 3);
            var class4 = serviceProvider.GetService(typeof(ITest2), "Test");
            Assert.True(class4 is Class4);
            var class3 = serviceProvider.GetService(typeof(ITest2), "Testa");
            Assert.True(class3 is null);
        }


        [Fact]
        public void Attribute_Throws_ArgumentNullException()
        {
            Exception ex = Assert.Throws<ArgumentNullException>(() => new InjectableAttribute(null, Enums.DependencyInjectionTypes.Scoped));
            Assert.Equal(Expected, ex.Message);
        }

        [Fact]
        public void Attribute_Constructor_Correctly()
        {
            InjectableAttribute injectable =  new InjectableAttribute(typeof(Class1), Enums.DependencyInjectionTypes.Scoped);
            Assert.NotNull(injectable);
            Assert.NotNull(injectable.InstaceOf);
        }

        [Fact]
        public void Authenticate_With_Invalid_Credentials_Throws_AuthenticationException()
        {
            Exception ex = Assert.Throws<ArgumentNullException>(() => IServiceCollectionExtension.ScanInjections(null));
            Assert.Equal(Expected1, ex.Message);
        }

        [Fact]
        public void Dependency_Injection_Types_Are_All() {
            Assert.True(Enum.IsDefined(typeof(DependencyInjectionTypes), "Scoped"));
            Assert.True(Enum.IsDefined(typeof(DependencyInjectionTypes), "Singlenton"));
            Assert.True(Enum.IsDefined(typeof(DependencyInjectionTypes), "Transient"));
        }
    }
}
