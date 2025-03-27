using H.Extensions.Setting;
using H.Services.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using H.Services.Setting;

namespace H.Test.SideMenu
{
    [Display(Name = "页面设置", GroupName = SettingGroupNames.GroupBase)]
    public class AppSetting : Settable<AppSetting>
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
