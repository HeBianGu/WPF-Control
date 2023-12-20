using H.Extensions.Setting;
using H.Providers.Ioc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace H.Controls.TagBox
{
    [Display(Name = "标签管理", GroupName = SystemSetting.GroupSystem, Description = "登录页面设置的信息")]
    public class TagOptions : IocOptionInstance<TagOptions>
    {
        public ObservableCollection<Tag> Tags { get; set; } = new ObservableCollection<Tag>();
        public string SplitChars { get; set; } = ", ";
    }

    //public class TagsJsonConverter : JsonConverter<ObservableCollection<ITag>>
    //{
    //    //public override Brush Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    //    //{
    //    //    string value = reader.GetString();
    //    //    return value.TryChangeType<Brush>();
    //    //}

    //    //public override void Write(Utf8JsonWriter writer, Brush value, JsonSerializerOptions options)
    //    //{
    //    //    if (value == null)
    //    //    {
    //    //        writer.WriteNull(string.Empty);
    //    //        return;
    //    //    }
    //    //    string txt = value.TryConvertToString();
    //    //    writer.WriteStringValue(txt);
    //    //}
    //    public override ObservableCollection<ITag> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void Write(Utf8JsonWriter writer, ObservableCollection<ITag> value, JsonSerializerOptions options)
    //    {
    //        if (value == null)
    //        {
    //            writer.WriteNull(string.Empty);
    //            return;
    //        }
          
    //        writer.WriteStartArray();
    //        foreach (var item in value)
    //        {
    //            writer.WriteStartObject();
    //            string txt = item.TryConvertToString();
    //            writer.WritePropertyName(txt);
    //            writer.WriteStringValue("");
    //        }
    //        writer.WriteEndArray();
    //    }
    //}


    //public class CaseInsensitivePropertyConverter<T> : JsonConverter<T>
    //{
    //    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    //    {
    //        if (reader.TokenType == JsonTokenType.StartObject)
    //        {
    //            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
    //            {
    //                var root = doc.RootElement;
    //                var obj = Activator.CreateInstance<T>();

    //                foreach (var property in root.EnumerateObject())
    //                {
    //                    var propertyName = property.Name;
    //                    var propertyValue = property.Value;

    //                    var propertyInfo = typeof(T).GetProperty(propertyName, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
    //                    if (propertyInfo != null)
    //                    {
    //                        object value = JsonSerializer.Deserialize(propertyValue.GetRawText(), propertyInfo.PropertyType);
    //                        propertyInfo.SetValue(obj, value);
    //                    }
    //                }
    //                return obj;
    //            }
    //        }

    //        throw new JsonException("Expected start of an object.");
    //    }

    //    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    //    {
    //        JsonSerializer.Serialize(writer, value, options);
    //    }
    //}
}
