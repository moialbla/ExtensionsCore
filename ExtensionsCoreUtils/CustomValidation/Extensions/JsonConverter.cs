using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ExtensionsCoreUtils.CustomValidation.Extensions
{
    internal class KeysJsonConverter : JsonConverter
    {


        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {

            Type _type = (Type) value;
            JObject validation = new JObject();

            foreach (var propertyInfo in _type
                                                  .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                                  .ToList()
                                                  .FindAll((p) => !(typeof(ValidationAttribute) is null)))
            {
                JObject validators = new JObject(new JProperty("validations", new JArray(
                    from _val in propertyInfo.GetCustomAttributes()
                    orderby (_val.TypeId as Type).Name
                    select GetAttribute(_val))));
                JProperty prop = new JProperty(propertyInfo.Name, validators);
                validation.Add(prop);
            }
            JProperty zclass = new JProperty(_type.Name, validation);
            zclass.WriteTo(writer);
        }

        private dynamic GetAttribute(object obj) {
            Type type = obj.GetType();
            String name = type.Name.Replace("Attribute", "");

            if (type.Equals(typeof(Attributes.CustomValidationAttribute))) {
                return new JObject(new JProperty(name,
                    GetCustomAttributesProperties((obj as Attributes.CustomValidationAttribute))));
            }
            return new JObject(new JProperty(name, 
                GetDataValidations((obj as RequiredAttribute))));
        }


        //"fn": "()=>{}",
        //"message": "testCustom"
        public JObject GetCustomAttributesProperties(Attributes.CustomValidationAttribute attribute) {
            JObject customAttributes = new JObject();
            JProperty fn = new JProperty("fn", attribute.FunctionValidation);
            JProperty fnName = new JProperty("fnName", attribute.FunctionName);
            JProperty message = new JProperty("message", attribute.ErrorMessage);
            customAttributes.Add(fn);
            customAttributes.Add(fnName);
            customAttributes.Add(message);
            return customAttributes;
        }

        private JObject GetDataValidations(RequiredAttribute attribute) {
            JObject customAttributes = new JObject();
            JProperty message = new JProperty("message", attribute.ErrorMessage);
            customAttributes.Add(message);
            return customAttributes;
        } 

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override bool CanRead
        {
            get { return false; }
        }

    }
}
