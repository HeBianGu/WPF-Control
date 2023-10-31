// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase


using H.Extensions.Setting;
using H.Providers.Ioc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Modules.Setting
{
    /// <summary> 登录 </summary>
    [Display(Name = "设置页面配置", GroupName = SystemSetting.GroupBase)]
    public class SettingViewSetting : Setting<LoginSetting>
    {
        private Thickness _margin;
        [DefaultValue(typeof(Thickness), "0,0,0,0")]
        [Display(Name = "设置页面边距")]
        public Thickness Margin
        {
            get { return _margin; }
            set
            {
                _margin = value;
                RaisePropertyChanged();
            }
        }
    }
}
