using H.Extensions.Setting;
using H.Services.Common;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace H.Extensions.NewtonsoftJson
{
    [Display(Name = "注册页面设置", GroupName = SettingGroupNames.GroupSystem, Description = "注册页面设置的信息")]
    public class NewtonsoftJsonOptions : IocOptionInstance<NewtonsoftJsonOptions>
    {
        public override void LoadDefault()
        {
            base.LoadDefault();
            this.JsonSerializerSettings = this.CreateSerializerSettings();
        }
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
                //Converters= { new TypeConverterJsonConverter() }//这部分序列化是会逻辑有问题用FilterBox测试

            };
            return setting;
        }

    }
}
