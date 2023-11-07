using H.Extensions.Setting;
using H.Providers.Ioc;
using Microsoft.Extensions.Options;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace H.Modules.Login
{
    [Display(Name = "登录页面", GroupName = SystemSetting.GroupSystem, Description = "登录页面设置的信息")]
    public class LoginOptions : IocOptionInstance<LoginOptions>
    {
        public string MyName { get; set; } = "我是名称";
    }
}
