using H.Extensions.ApplicationBase;
using H.Modules.Setting;
using H.Services.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace H.Test.Setting
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : ApplicationBase
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddAdornerDialogMessage();
            //services.AddWindowDialogMessage();
            services.AddSetting();
        }

        protected override void Configure(IApplicationBuilder app)
        {
            app.UseSettingDataManager(x =>
            {
                x.Add(LoginSetting.Instance);
            });
            app.UseSetting();
            app.UseSettingDefault();
        }

        protected override Window CreateMainWindow(StartupEventArgs e)
        {
            return new MainWindow();
        }

        protected override void OnSplashScreen(StartupEventArgs e)
        {
            base.OnSplashScreen(e);

            SettingDataManager.Instance.Load(null, out var message);
        }
    }
}
