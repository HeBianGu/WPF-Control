using H.Extensions.Setting;
using H.Providers.Ioc;
using H.Themes.Default;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace H.Modules.Theme
{
    [Display(Name = "主题设置", GroupName = SystemSetting.GroupSystem, Description = "登录页面设置的信息")]
    public class SwitchThemeOptions : IocOptionInstance<SwitchThemeOptions>
    {
        [DefaultValue(true)]
        [Display(Name = "深主题")]
        public bool IsDark { get; set; }

        [Browsable(false)]
        [JsonIgnore]
        [XmlIgnore]
        public IColorResource Dark { get; set; } = new DarkColorResource();

        [Browsable(false)]
        [JsonIgnore]
        [XmlIgnore]
        public IColorResource Light { get; set; } = new LightColorResource ();
    }
}
