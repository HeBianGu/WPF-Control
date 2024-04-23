// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Providers.Ioc;
using H.Providers.Mvvm;

namespace H.Extensions.Setting
{
    public class ClearCacheDataCommand : MarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            AppPaths.Instance.ClearCache();
        }
    }
}
