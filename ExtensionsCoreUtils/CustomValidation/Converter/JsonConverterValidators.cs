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

        internal dynamic GetValidators(PropertyInfo propertyInfo) {
            JObject validators = new JObject(new JProperty("validations", new JArray(
                   from _val in propertyInfo.GetCustomAttributes()
                   orderby (_val.TypeId as Type).Name
                   select GetAttribute(_val, propertyInfo))));
            return validators;
        }

        internal dynamic GetAttribute(object obj, PropertyInfo propertyInfo)
        {
            Type type = obj.GetType();
            String name = type.Name.FirstCharacterToLower().Replace("Attribute", "").ToLowerInvariant();

            if (type.Equals(typeof(Attributes.CustomValidationAttribute)))
            {
                return new JObject(new JProperty(name,
                    GetCustomAttributesProperties((obj as Attributes.CustomValidationAttribute))));
            }
            else
            {
                return new JObject(new JProperty(name,
                    GetDataValidations(type, obj)));
            }
        }

        private JObject GetCustomAttributesProperties(Attributes.CustomValidationAttribute attribute)
        {
            JObject customAttributes = new JObject();
            JProperty fn = new JProperty("fn", attribute.FunctionValidation);
            JProperty fnName = new JProperty("fnName", attribute.FunctionName);
            JProperty message = new JProperty("message", attribute.ErrorMessage);
            customAttributes.Add(fn);
            customAttributes.Add(fnName);
            customAttributes.Add(message);
            return customAttributes;
        }

        private JObject GetDataValidations(Type propertyInfo, object obj) {
            JObject customAttributes = new JObject();   

            foreach (PropertyInfo prop in propertyInfo
                                        .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty)
                                        .Where(p => !p.PropertyType.IsClass))
            {
                var propertyName = prop.Name;
                var propertyValue = propertyInfo.GetProperty(propertyName).GetValue(obj, null);
                if (propertyValue != null)
                {
                    customAttributes.Add(new JProperty(propertyName.FirstCharacterToLower(), propertyValue));
                }
            }

            return customAttributes;
        }

    }
}
