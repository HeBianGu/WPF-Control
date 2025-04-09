using H.Extensions.ApplicationBase;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace H.Test.SplashScreen
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : ApplicationBase
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddWindowMessage();
            services.AddWindowDialogMessage();
            services.AddSplashScreen();
            services.AddSetting();
        }

        protected override void Configure(IApplicationBuilder app)
        {
            base.Configure(app);
            app.UseSplashScreenOptions();
        }

        protected override Window CreateMainWindow(StartupEventArgs e)
        {
            return new MainWindow();
        }
    }
}
