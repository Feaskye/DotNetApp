using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;

namespace ConsoleDemo.Helpers
{
    public class ConverterContractResolver: DefaultContractResolver
    {
        public new static readonly ConverterContractResolver Instance = new ConverterContractResolver();

        protected override JsonContract CreateContract(Type objectType)
        {
            JsonContract contract = base.CreateContract(objectType);
            if (objectType == typeof(String))
            {
                contract.Converter = new StringConverter();
            }
            //if (objectType == typeof(DateTime) || objectType == typeof(DateTimeOffset))
            //{
            //    contract.Converter = new JavaScriptDateTimeConverter();
            //}
            return contract;
        }

        //protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        //{
        //    JsonProperty property = base.CreateProperty(member, memberSerialization);
        //    if (property.PropertyType.Name == typeof(String).Name)
        //    {
        //        property.Converter = new StringConverter();
        //    }
        //    return base.CreateProperty(member, memberSerialization);
        //}

    }
}
