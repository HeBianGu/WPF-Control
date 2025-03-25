using Newtonsoft.Json;
using System.Reflection;
using System.Windows.Input;

namespace H.Extensions.NewtonsoftJson.Jsonable;


public abstract class NewtonsoftJsonableBase : IJsonable
{
    public virtual void ReadJson(JsonReader reader, JsonSerializer serializer, object existingValue)
    {
        // 填充属性
        serializer.Populate(reader, this);
        //// 读取 JSON 对象
        //var jsonObject = JObject.Load(reader);
        ////// 获取类型信息
        ////var type = jsonObject["Type"].ToString();

        ////serializer.Populate(jsonObject.CreateReader(), this);
        ////serializer.Deserialize(jsonObject.CreateReader(), this.GetType());
    }

    public virtual void WriteJson(JsonWriter writer, JsonSerializer serializer)
    {
        serializer.Serialize(writer, this);
        ////var jsonObject = new JObject();
        //////var typeAttribute = value.GetType().GetCustomAttributes(typeof(JsonTypeAttribute), false)[0] as JsonTypeAttribute;
        //////jsonObject["Type"] = typeAttribute?.TypeName;

        ////foreach (var prop in value.GetType().GetProperties())
        ////{
        ////    jsonObject[prop.Name] = JToken.FromObject(prop.GetValue(value), serializer);
        ////}

        ////jsonObject.WriteTo(writer);
        //////writer.WritePropertyName("Value");
        //////writer.WriteValue(value);
        //////serializer.Serialize(writer, value);
        //// 写入类型信息
        //writer.WriteStartObject();
        ////writer.WritePropertyName("Type");
        ////writer.WriteValue(value.GetType().Name);

        //// 写入其他属性
        //foreach (var prop in this.GetJsonPropertyInfos())
        //{
        //    if (prop.Name != "Type") // 避免重复写入 Type
        //    {
        //        writer.WritePropertyName(prop.Name);
        //        serializer.Serialize(writer, prop.GetValue(this));
        //    }
        //}
        //writer.WriteEndObject();
    }

    protected virtual IEnumerable<PropertyInfo> GetJsonPropertyInfos()
    {
        var result = this.GetType().GetProperties().Where(x => x.GetCustomAttribute<JsonIgnoreAttribute>() == null);
        return result.Where(x => !typeof(ICommand).IsAssignableFrom(x.PropertyType));
    }
}
