using H.Extensions.Setting;
using H.Providers.Ioc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace H.Modules.SplashScreen
{
    [Display(Name = "启动页面", GroupName = SystemSetting.GroupSystem, Description = "启动页面设置信息")]
    public class SplashScreenOption : IocOptionInstance<SplashScreenOption>
    {
        public string MyName { get; set; }
    }
}
