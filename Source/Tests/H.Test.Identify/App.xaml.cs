using H.DataBases.Share;
using H.Extensions.ApplicationBase;
using H.Modules.Identity;
using H.Modules.Operation;
using H.Services.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using H.Extensions.DataBase;

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
            services.AddWindowMessage();
            services.AddAdornerDialogMessage();
            services.AddFormMessageService();
            //services.AddLoginViewPresenter();
            

            //  Do ：身份认证
            services.AddDbContextBySetting<IdentifyDataContext>();
            services.AddSingleton<IStringRepository<hi_dd_user>, DbContextRepository<IdentifyDataContext, hi_dd_user>>();
            services.AddUserViewPresenter();

            services.AddSingleton<IStringRepository<hi_dd_role>, DbContextRepository<IdentifyDataContext, hi_dd_role>>();
            services.AddRoleViewPresenter();

            services.AddSingleton<IStringRepository<hi_dd_author>, DbContextRepository<IdentifyDataContext, hi_dd_author>>();
            services.AddAuthorityViewPresenter();

            //  Do ：操作日志
            services.AddDbContextBySetting<OperationDataContext>();
            services.AddSingleton<IStringRepository<hi_dd_operation>, DbContextRepository<OperationDataContext, hi_dd_operation>>();
            services.AddOperationViewPresenter();

            //  Do ：登录和注册页面
            services.AddRegisterLoginViewPresenter();
            services.AddLoginService();
            services.AddRegisterService();
            services.AddSplashScreen();
            services.AddTheme();
        }

        protected override void Configure(IApplicationBuilder app)
        {
            base.Configure(app);
            app.UseLoginOptions();
            app.UseRegistorOptions(x => x.UseMail = false);
            app.UseThemeOptions();
            app.UseSqlite();
        }
    }
}
