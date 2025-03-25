// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Common.Setting;

public class SumitSettingDataCommand : DialogCommandBase
{
    public override void Execute(object parameter)
    {
        IocSetting.Instance.Save(out string message);
        this.Sumit(parameter);
    }
}
