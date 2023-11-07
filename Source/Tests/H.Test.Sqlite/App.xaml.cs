using H.DataBases.Sqlite;
using H.Extensions.ApplicationBase;
using H.Providers.Ioc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace H.Test.Sqlite
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : ApplicationBase
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddSetting();
            services.AddMessage();
            services.AddDbContextBySetting<MyDataContext>();
            services.AddLogging(configure =>
            {

            });

            services.AddMemoryCache(x =>
            {
                x.TrackLinkedCacheEntries = true;
            });
        }

        protected override Window CreateMainWindow(StartupEventArgs e)
        {
            return new MainWindow();
        }

        protected override void OnSplashScreen(StartupEventArgs e)
        {
            base.OnSplashScreen(e);

            var r = Ioc.GetService<IDbConnectService>().Load(out string error);
        }
    }
}
