using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SitefinityNET.Sitefinity.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitefinityNET.JSON
{
    public class LstringConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string stringValue = value as string;

            Lstring lstring = new Lstring()
            {
                Value = "",
                PersistedValue = stringValue
            };

            serializer.Serialize(writer, lstring);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                return (string)reader.Value;
            }
            else
            {
                JObject jsonObject = JObject.Load(reader);
                var properties = jsonObject.Properties().ToList();

                return (string)properties[0].Value;
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Lstring).IsAssignableFrom(objectType);
        }
    }
}