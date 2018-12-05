using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDemo.Helpers
{
    public class StringConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(String).IsAssignableFrom(objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                var str = (String)value;

                writer.WriteStartObject();
                writer.WritePropertyName(writer.Path);
                writer.WriteValue(string.Empty);
                writer.WriteEndObject();
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return (string)reader.Value;
        }
    }
}
