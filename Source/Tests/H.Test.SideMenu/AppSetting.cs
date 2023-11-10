using H.Extensions.Setting;
using H.Providers.Ioc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace H.Test.SideMenu
{
    [Display(Name = "页面设置", GroupName = SystemSetting.GroupBase)]
    public class AppSetting : Setting<AppSetting>
    {
        private bool _useGroup;
        /// <summary> 开机自动启动  </summary>
        [DefaultValue(false)]
        [Display(Name = "启用菜单分组")]
        public bool UseGroup
        {
            get { return _useGroup; }
            set
            {
                _useGroup = value;
                RaisePropertyChanged();
            }
        }
    }
}
