// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using H.Providers.Ioc;
using H.Providers.Mvvm;
using H.Providers.Mvvm;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;

namespace H.Extensions.Setting
{
    public class ClearCacheDataCommand : MarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            SystemPathSetting.Instance.ClearCache();
        }
    }

    public class ClearSettingDataCommand : MarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            SystemPathSetting.Instance.ClearSetting();
        }
    }
}
