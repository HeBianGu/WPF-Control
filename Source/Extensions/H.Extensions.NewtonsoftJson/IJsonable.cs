using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;

namespace H.Extensions.NewtonsoftJson
{
    public interface IJsonable
    {
        object ReadJson(JsonReader reader, JsonSerializer serializer, object existingValue);

        void WriteJson(JsonWriter writer, JsonSerializer serializer);

    }

    public class JsonableJsonConverter : JsonConverter
    {
        IJsonable CreateJsonable(Type objectType)
        {
            return Activator.CreateInstance(objectType) as IJsonable;
        }
        public override bool CanConvert(Type objectType)
        {
            return typeof(IJsonable).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;
            var jsonable = CreateJsonable(objectType);
            return jsonable.ReadJson(reader, serializer, existingValue);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is IJsonable jsonable)
            {
                jsonable.WriteJson(writer, serializer);
                return;
            }
            writer.WriteNull();
            //var jsonObject = new JObject();
            ////var typeAttribute = value.GetType().GetCustomAttributes(typeof(JsonTypeAttribute), false)[0] as JsonTypeAttribute;
            ////jsonObject["Type"] = typeAttribute?.TypeName;

            //foreach (var prop in value.GetType().GetProperties())
            //{
            //    jsonObject[prop.Name] = JToken.FromObject(prop.GetValue(value), serializer);
            //}

            //jsonObject.WriteTo(writer);
        }
    }

    public class MyJsonable : JsonableBase
    {
        public int Value { get; set; } = 222;
        public override object ReadJson(JsonReader reader, JsonSerializer serializer, object existingValue)
        {
            return base.ReadJson(reader, serializer, existingValue);
        }

        public override void WriteJson(JsonWriter writer, JsonSerializer serializer)
        {
            base.WriteJson(writer, serializer);
        }
    }
}
