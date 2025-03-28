
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

namespace H.Test.Ribbon
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
            services.AddSwitchThemeViewPresenter();
        }

        protected override void Configure(IApplicationBuilder app)
        {
            app.UseSettingView();
            app.UseSettingDefault();
        }

        protected override Window CreateMainWindow(StartupEventArgs e)
        {
            return new MainWindow();
        }
    }
}
