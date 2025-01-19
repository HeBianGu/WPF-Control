using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace H.Extensions.NewtonsoftJson
{
    public class TypeConverterJsonConverter<T> : JsonConverter where T : TypeConverter
    {
        T CreateTypeConverter()
        {
            return Activator.CreateInstance<T>();
        }
        public override bool CanConvert(Type objectType)
        {
            TypeConverter converter = CreateTypeConverter();
            return converter != null && converter.CanConvertFrom(typeof(string)) && converter.CanConvertTo(typeof(string));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            TypeConverter converter = CreateTypeConverter();
            return converter.ConvertFromInvariantString((string)reader.Value);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            TypeConverter converter = CreateTypeConverter();
            writer.WriteValue(converter.ConvertToInvariantString(value));
        }

    }

    public class TypeConverterJsonConverter: JsonConverter
    {
        TypeConverter CreateTypeConverter(Type objectType)
        {
            return TypeDescriptor.GetConverter(objectType);
        }
        public override bool CanConvert(Type objectType)
        {
            return true;
            //TypeConverter converter = CreateTypeConverter(objectType);
            //return converter != null && converter.CanConvertFrom(typeof(string)) && converter.CanConvertTo(typeof(string));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            TypeConverter converter = CreateTypeConverter(objectType);
            if(converter==null)
                return reader.Value;
            return converter.ConvertFromInvariantString((string)reader.Value);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            TypeConverter converter = CreateTypeConverter(value.GetType());
            if (converter == null)
                writer.WriteValue(value);
            writer.WriteValue(converter.ConvertToInvariantString(value));
        }

    }
}
