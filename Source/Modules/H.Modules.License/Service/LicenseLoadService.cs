// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



using H.Services.Common;
using H.Services.Common.SplashScreen;
using H.Services.Message;
using H.Services.Message.Dialog;
using Microsoft.Extensions.Options;
using System.Windows;

namespace H.Modules.License
{
    public class LicenseLoadService : ILicenseLoadService
    {
        IOptions<LicenseOptions> _options;
        public LicenseLoadService(IOptions<LicenseOptions> options)
        {
            _options = options;
        }

        public string Name => "许可设置";

        public bool Load(out string message)
        {
            message = null;
            if (LicenseProxy.Instance == null)
                return true;
            if (this._options.Value.UseVailLicenceOnLoad == false)
                return true;

            var option = LicenseProxy.Instance.IsVail(out string error);
            if (option == null)
            {
                return Application.Current.Dispatcher.Invoke(() =>
                 {
                     return IocMessage.Window.Show(new LicenseViewPresenter(), x =>
                     {
                         x.Title = "许可";
                         x.MinWidth = 700;
                         x.DialogButton = DialogButton.None;
                     }).Result.Value;
                 });
            }
            if (option.Level == -1)
            {
                if (this._options.Value.UseTipTrialOnLoad == false)
                    return true;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    IocMessage.Window.Show(new LicenseViewPresenter(), x =>
                    {
                        x.Title = "许可";
                        x.MinWidth = 700;
                        x.DialogButton = DialogButton.None;
                    }).Wait();
                });
            }
            return true;
        }
    }
}