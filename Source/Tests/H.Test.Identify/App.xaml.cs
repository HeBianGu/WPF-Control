using H.DataBases.Share;
using H.Extensions.ApplicationBase;
using H.Extensions.ViewModel;
using H.Modules.Identity;
using H.Modules.Operation;
using H.Providers.Ioc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace H.Test.Identify
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
            services.AddSetting();
            services.AddMessage();
            services.AddLoginViewPresenter();
            services.AddSplashScreen();

            services.AddDbContextBySetting<IdentifyDataContext>();

            services.AddSingleton<IStringRepository<hi_dd_user>, DbContextRepository<IdentifyDataContext, hi_dd_user>>();
            services.AddUserViewPresenter();
            services.AddLoginService();

            services.AddSingleton<IStringRepository<hi_dd_role>, DbContextRepository<IdentifyDataContext, hi_dd_role>>();
            services.AddRoleViewPresenter();

            services.AddSingleton<IStringRepository<hi_dd_author>, DbContextRepository<IdentifyDataContext, hi_dd_author>>();
            services.AddAuthorityViewPresenter();

            services.AddDbContextBySetting<OperationDataContext>();
            services.AddSingleton<IStringRepository<hi_dd_operation>, DbContextRepository<OperationDataContext, hi_dd_operation>>();
            services.AddOperationViewPresenter();
        }

        protected override void Configure(IApplicationBuilder app)
        {
            base.Configure(app);
            app.UseLoginSetting();
        }
    }
}
