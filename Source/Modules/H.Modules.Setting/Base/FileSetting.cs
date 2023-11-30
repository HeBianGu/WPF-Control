// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Extensions.Setting;
using H.Providers.Ioc;
using System;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Setting
{
    /// <summary> 文件管理 </summary>
    [Display(Name = "文件管理", GroupName = SystemSetting.GroupBase)]
    public class FileSetting : Setting<FileSetting>
    {
    }

    [Display(Name = "密码", GroupName = SystemSetting.GroupSecurity)]
    public class PasswordSetting : Setting<PasswordSetting>
    {

    }

    [Display(Name = "消息记录", GroupName = SystemSetting.GroupMessage)]
    public class MessageSetting : Setting<MessageSetting>
    {

    }


    [Display(Name = "个人资料", GroupName = SystemSetting.GroupSecurity)]
    public class PersonalSetting : Setting<PersonalSetting>
    {

    }
}
