﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Extensions.Setting;
using H.Services.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Modules.Setting
{
    /// <summary> 登录 </summary>
    [Display(Name = "设置页面配置", GroupName = SettingGroupNames.GroupBase)]
    public class SettingViewSetting : Settable<LoginSetting>
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
