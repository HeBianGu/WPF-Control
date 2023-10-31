// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase


using H.Providers.Ioc;
using H.Providers.Mvvm;

namespace H.Modules.Setting
{
    public class ShowSettingCommand : MarkupCommandBase
    {
        public override async void Execute(object parameter)
        {
            var setting = new SettingViewPresenter();
            var r = await IocMessage.Dialog.Show(setting, x =>
            {
                x.Width = 800;
                x.Height = 500;
            }, DialogButton.None);
            if (r != true)
                return;

            var sr = SettingDataManager.Instance.Save(out string error);
            if (sr == false)
            {
                await IocMessage.Dialog.ShowMessage(error);
            }
        }
    }

    public class SettingDefaultCommand : MarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            SettingDataManager.Instance.SetDefault();
        }
    }

    public class CancelSettingCommand : MarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            SettingDataManager.Instance.Cancel();
        }
    }
}
