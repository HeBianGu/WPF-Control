using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text.Json.Nodes;

namespace H.Extensions.NewtonsoftJson
{
    public class JsonableBase : IJsonable
    {
        public virtual object ReadJson(JsonReader reader, JsonSerializer serializer, object existingValue)
        {
            // 读取 JSON 对象
            var jsonObject = JObject.Load(reader);
            //// 获取类型信息
            //var type = jsonObject["Type"].ToString();
            // 填充属性
            serializer.Populate(jsonObject.CreateReader(), this);
            return this;
        }

        public virtual void WriteJson(JsonWriter writer, JsonSerializer serializer)
        {
            //var jsonObject = new JObject();
            ////var typeAttribute = value.GetType().GetCustomAttributes(typeof(JsonTypeAttribute), false)[0] as JsonTypeAttribute;
            ////jsonObject["Type"] = typeAttribute?.TypeName;

            //foreach (var prop in value.GetType().GetProperties())
            //{
            //    jsonObject[prop.Name] = JToken.FromObject(prop.GetValue(value), serializer);
            //}

            //jsonObject.WriteTo(writer);
            ////writer.WritePropertyName("Value");
            ////writer.WriteValue(value);
            ////serializer.Serialize(writer, value);
            // 写入类型信息
            writer.WriteStartObject();
            //writer.WritePropertyName("Type");
            //writer.WriteValue(value.GetType().Name);

            // 写入其他属性
            foreach (var prop in this.GetType().GetProperties())
            {
                if (prop.Name != "Type") // 避免重复写入 Type
                {
                    writer.WritePropertyName(prop.Name);
                    serializer.Serialize(writer, prop.GetValue(this));
                }
            }
            writer.WriteEndObject();
        }
    }
}
