// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Services.Common;

using H.Mvvm;

namespace H.Modules.Setting
{
    public class CancelSettingCommand : MarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            SettingDataManager.Instance.Cancel();
        }
    }
}
