// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Services.Common;
using H.Mvvm;
using System.Threading.Tasks;

namespace H.Extensions.Setting
{
    public class CancelSettingDataCommand : DialogCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            var r = await IocMessage.ShowDialogMessage("取消配置将不会保存，是否继续？");
            if (r == false)
                return;
            SettingDataManager.Instance.Load(null, out string message);
            this.Cancel(parameter);
        }
    }
}
