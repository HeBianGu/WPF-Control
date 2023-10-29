using H.Extensions.ApplicationBase;
using H.Modules.Login;
using H.Modules.SplashScreen;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace H.Test.ApplicationBase
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : H.Extensions.ApplicationBase.ApplicationBase
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ITest, Test>();
        }

        protected override System.Windows.Window CreateMainWindow(StartupEventArgs e)
        {
            return new MainWindow();
        }

        protected override void OnSplashScreen(StartupEventArgs e)
        {
            new SplashScreenWindow().ShowDialog();

        }

        protected override void OnLogin(StartupEventArgs e)
        {
            new LoginWindow().ShowDialog();
        }
    }
}
