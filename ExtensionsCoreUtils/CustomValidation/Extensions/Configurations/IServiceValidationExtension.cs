using ExtensionsCoreUtils.Attributes;
using ExtensionsCoreUtils.CustomValidation.Extensions;
using ExtensionsCoreUtils.Utils;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{

    public static class IServiceValidationExtension
    {

        static readonly Dictionary<string, string> objectList = new Dictionary<string, string>();

        public static IServiceCollection ScanValidations(this IServiceCollection services, params string[] assemblies)
        {
            GenerateValidations(CoreUtils
                .GetListOfTypes(assemblies, 
                                (t)=> !(t.GetCustomAttribute(typeof(DomainValidationNameAttribute)) is null) ));
            return services;

        }

        public static Dictionary<string, string> ValidationList(this IServiceCollection services)
        {
            return objectList;
        }

        private static void GenerateValidations(IEnumerable<Type> types)
        {
            foreach (Type zclass in types)
            {



                string json = JsonConvert.SerializeObject(zclass, Formatting.Indented, new KeysJsonConverter(zclass));

                foreach (var propertyInfo in zclass
                                .GetProperties(
                                        BindingFlags.Public
                                        | BindingFlags.Instance))
                {
                    // do stuff here
                }


                objectList.Add(GetName(zclass), json);
                System.Diagnostics.Debug.WriteLine($"{GetName(zclass)}, {json}");
            }
        }


        private static string GetName(Type type) {
            return ((DomainValidationNameAttribute)type.GetCustomAttribute(typeof(DomainValidationNameAttribute))).Name ?? type.Name;
        }

        /**
         {
  "property": {
    "validations": [
      {
        "required": {
          "arguments": [
            1,
            2,
            3
          ],
          "message": "test"
        }
      },
      {
        "customFunction": {
          "fn": "()=>{}",
          "message": "testCustom"
        }
      },
      {
        "length": {
          "arguments": [
            1,
            3
          ],
          "message": "test"
        }
      }
    ]
  },
  "property1": {
    "validations": [
      {
        "required": {
          "arguments": [
            1,
            2,
            3
          ],
          "message": "test"
        }
      },
      {
        "customFunction": {
          "fn": "()=>{}",
          "message": "testCustom"
        }
      },
      {
        "length": {
          "arguments": [
            1,
            3
          ],
          "message": "test"
        }
      }
    ]
  }
}
         */


    }
}
