// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;

namespace H.Extensions.Setting
{
    public abstract class IocSettingInstance<Setting, Interface> : SettingBase where Setting : class, Interface, new()
    {
        public static Setting Instance => (Setting)Ioc.GetService<Interface>();
    }
}
