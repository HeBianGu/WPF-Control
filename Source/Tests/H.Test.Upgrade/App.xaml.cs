
using H.Extensions.ApplicationBase;
using H.Services.Setting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace H.Test.Upgrade
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : ApplicationBase
    {
        protected override Window CreateMainWindow(StartupEventArgs e)
        {
            return new MainWindow();
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            
            services.AddSetting();
            services.AddWindowMessage();
            services.AddWindowDialogMessage();
            services.AddSplashScreen();
            //  Do ：注册软件更新页面
            services.AddAutoUpgrade(x =>
            {
                //x.Uri = "https://gitee.com/hebiangu/wpf-auto-update/raw/master/Install/Diagram/AutoUpdate.xml";
                x.Uri = "https://gitee.com/hebiangu/wpf-auto-update/raw/master/Install/Movie/Movie.xml";
                x.UseIEDownload = false;
            });
        }

        protected override void Configure(IApplicationBuilder app)
        {
            base.Configure(app);
            app.UseUpgrade();
        }
    }
}
