
using H.Extensions.ApplicationBase;
using H.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using H.Iocable;
using H.Services.Common.DataBase;

namespace H.Test.SqlServer
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
            services.AddDbContextBySetting<MyDataContext>();
        }

        protected override Window CreateMainWindow(StartupEventArgs e)
        {
            return new MainWindow();
        }

        protected override void Configure(IApplicationBuilder app)
        {
            base.Configure(app);
            app.UseSqlServer();
        }

        protected override void OnSplashScreen(StartupEventArgs e)
        {
            base.OnSplashScreen(e);

            var loads = Ioc.Services.GetServices<IDbConnectService>();
            foreach (var load in loads)
            {
                load.Load(out string error);
            }
        }
    }
}
