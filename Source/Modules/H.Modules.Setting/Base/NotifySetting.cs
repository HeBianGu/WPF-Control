// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using H.Extensions.Setting;
using H.Providers.Ioc;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Setting
{
    /// <summary> 提醒 </summary>
    [Display(Name = "提醒", GroupName = SystemSetting.GroupMessage)]
    public class NotifySetting : Setting<NotifySetting>
    {
    }
}
