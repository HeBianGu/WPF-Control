// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using Microsoft.Extensions.Options;
using System;

namespace H.Extensions.Setting
{
    public abstract class IocOptionInstance<Setting> : SettingBase where Setting : class, new()
    {
        public static Setting Instance => Ioc.GetService<IOptions<Setting>>().Value;
    }
}
