using H.Extensions.ApplicationBase;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace H.Test.Ioc
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : ApplicationBase
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ITest, MyTest>();
            services.AddAbout(x => x.ProductName = "My ProductName");
        }

        protected override Window CreateMainWindow(StartupEventArgs e)
        {
            return new MainWindow();
        }

        protected override void OnSplashScreen(StartupEventArgs e)
        {
            new Window() { Content = MethodInfo.GetCurrentMethod().Name, Width = 400, Height = 200, FontSize = 50 }.ShowDialog();

        }

        protected override void OnLogin(StartupEventArgs e)
        {
            new Window() { Content = MethodInfo.GetCurrentMethod().Name, Width = 400, Height = 200, FontSize = 50 }.ShowDialog();
        }
    }
}
