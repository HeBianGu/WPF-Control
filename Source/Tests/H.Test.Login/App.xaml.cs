using H.Extensions.ApplicationBase;
using H.Modules.Login;
using H.Providers.Ioc;
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
            services.AddMessage();
            services.AddLogin(x => x.Name = "你好配置的名称");
        }

        protected override Window CreateMainWindow(StartupEventArgs e)
        {
            return new MainWindow();
        }

        protected override void OnSplashScreen(StartupEventArgs e)
        {


        }

        protected override void OnLogin(StartupEventArgs e)
        {
            IocMessage.Dialog.ShowIoc<ILoginViewPresenter>();
        }
    }
}
