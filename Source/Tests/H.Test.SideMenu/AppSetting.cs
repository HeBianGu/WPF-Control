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

        private bool _useDataGroup;
        [DefaultValue(false)]
        [Display(Name = "启用数据分组")]
        public bool UseDataGroup
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
