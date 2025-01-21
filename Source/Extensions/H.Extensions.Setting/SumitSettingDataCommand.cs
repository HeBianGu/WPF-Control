// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Services.Common;
using H.Mvvm;
using System.Threading.Tasks;

namespace H.Extensions.Setting
{
    public class SumitSettingDataCommand : DialogCommandBase
    {
        public override void Execute(object parameter)
        {
            SettingDataManager.Instance.Save(out string message);
            this.Sumit(parameter);
        }
    }
}
