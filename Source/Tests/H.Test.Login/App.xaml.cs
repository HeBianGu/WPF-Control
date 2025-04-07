using H.Extensions.ApplicationBase;
using H.Extensions.Mail;
using H.Modules.Login;
using H.Modules.Setting;
using H.Services.Common;
using H.Services.Setting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace H.Test.Login
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : ApplicationBase
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            
            services.AddSetting();
            services.AddWindowMessage();
            services.AddAdornerDialogMessage();
            //services.AddLoginViewPresenter();
            services.AddRegisterLoginViewPresenter();
            services.AddTestLoginService();
            services.AddTestRegistorService();
            services.AddMail();
        }

        protected override Window CreateMainWindow(StartupEventArgs e)
        {
            return new MainWindow();
        }

        protected override void Configure(IApplicationBuilder app)
        {
            base.Configure(app);
            app.UseLoginOptions();
            app.UseRegistorOptions();
            app.UseMailOptions();
        }

        protected override void OnSplashScreen(StartupEventArgs e)
        {
            base.OnSplashScreen(e);
        }

        protected override void OnLogin(StartupEventArgs e)
        {
            base.OnLogin(e);

        }
    }
}
