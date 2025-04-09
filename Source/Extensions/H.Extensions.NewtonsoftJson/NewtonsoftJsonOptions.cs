using H.Extensions.NewtonsoftJson.Jsonable;
using H.Extensions.Setting;
using H.Services.Setting;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace H.Extensions.NewtonsoftJson;

[Display(Name = "注册页面设置", GroupName = SettingGroupNames.GroupSystem, Description = "注册页面设置的信息")]
public class NewtonsoftJsonOptions : IocOptionInstance<NewtonsoftJsonOptions>, INewtonsoftJsonOptions
{
    public override void LoadDefault()
    {
        base.LoadDefault();
        this.JsonSerializerSettings = this.CreateSerializerSettings();
    }
    [Browsable(false)]
    [JsonInclude]
    public JsonSerializerSettings JsonSerializerSettings { get; set; }

    private JsonSerializerSettings CreateSerializerSettings()
    {
        var setting = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,//默认值不保存,如果不加DefaultValue则跟类型默认值关连，加了之后就会根据DefaultValue关联
            Formatting = Formatting.Indented,// 增加格式化选项
                                             // 以允许命名的浮点文字
                                             //NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals，
                                             //ReferenceLoopHandling = ReferenceLoopHandling.Ignore //忽略循环引用
            ContractResolver = new CustomContractResolver(), // 使用自定义的ContractResolver忽略字段
            Converters = {
                new TypeConverterJsonConverter(),
                new EnumConverter(),
                new DateTimeConverter(),
                new JsonableJsonConverter() }//这部分序列化是会逻辑有问题用FilterBox测试

        };
        return setting;
    }

}

public class DateTimeConverter : Newtonsoft.Json.JsonConverter
{
    private readonly string _format = "yyyy-MM-dd HH:mm:ss";

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(DateTime);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
    {
        var str = (string)reader.Value;
        return DateTime.ParseExact(str, _format, null);
    }

    public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
    {
        var dateTime = (DateTime)value;
        writer.WriteValue(dateTime.ToString(_format));
    }
}
public class EnumConverter : Newtonsoft.Json.JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType.IsEnum;
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
    {
        string str = (string)reader.Value;
        return Enum.Parse(objectType, str);
    }

    public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString());
    }
}
