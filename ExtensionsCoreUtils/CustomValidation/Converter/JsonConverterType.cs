using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Linq;
using ExtensionsCoreUtils.Utils;

namespace ExtensionsCoreUtils.CustomValidation.Extensions
{
    internal partial class KeysJsonConverter
    {
        internal dynamic GetType(PropertyInfo propertyInfo) {
           return new JObject( new JProperty("type", new JValue(propertyInfo.PropertyType.Name.FirstCharacterToLower())));
        }
    }
}
