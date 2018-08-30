using ExtensionsCoreUtils.Utils;
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
    internal partial class KeysJsonConverter : JsonConverter
    {

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Type _type = (Type) value;
            JObject body = new JObject();
           
            foreach (var propertyInfo in _type
                                                  .GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance)
                                                  .ToList()
                                                  .FindAll((p) => !(typeof(ValidationAttribute) is null)))
            {
                body.Add(new JProperty(propertyInfo.Name.FirstCharacterToLower(), 
                    new JArray(
                    GetValidators(propertyInfo),
                    GetType(propertyInfo))));
            }
            JProperty zclass = new JProperty(_type.Name.FirstCharacterToLower(), body);
            zclass.WriteTo(writer);
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
