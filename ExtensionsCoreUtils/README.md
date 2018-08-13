# ExtensionsCoreUtils
---

## Introduction

ExtensionsCoreUtils is a bundle of utilities that focus in optimize the more tedious configurations tasks with one o two lines of code.  

## Features

#### **Dependecy injection**

Currently, version 2.x, we need to configurate the D.I. a interfece and class to implements. 
When we have this, we need to configure in **ConfigureServices** on start up application.
If you have 100 interfaces you need 100 configurations. 
Unbelievable! with this utiilty you be able to decorate all classes and configure a autoscan. All clases will be injected into D.I.

##### Usage

Very simple, the only what you need is decrotate with attribute and reference the interfece and done!

Example
```csharp
namespace Myspace.Interfaces
{
    interface ITest1 { }
}


namespace Myspace.Services
{
    [InjectableAttribute(typeof(ITest1), DependencyInjectionTypes.Scoped)]
    class Class1 : ITest1
    {
    }
}


namespace MyApp
{
    public class Startup
    {
        ......

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ...
            //Add this line in your configuration services.
            services.ScanInjections("Myspace");
            ...
        }
  ........
}
```