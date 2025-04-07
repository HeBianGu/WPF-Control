using H.Extensions.ApplicationBase;
using H.Modules.Setting;
using H.Services.Setting;
using H.Styles;
using H.Styles;
using H.Styles.Controls;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace H.Test.Controls
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
            services.AddAdornerDialogMessage();
            services.AddSnackMessage();
            //services.AddWindowDialogMessage();
            services.AddSetting();
        }

        protected override void Configure(IApplicationBuilder app)
        {
            app.UseWindowSetting();
            app.UseScrollViewerSetting();
            app.UseStyleOptions();
        }

    }
}
