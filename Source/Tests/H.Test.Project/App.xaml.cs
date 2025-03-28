using H.DataBases.Share;
using H.Extensions.ApplicationBase;
using H.Modules.Identity;
using H.Services.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using H.Extensions.DataBase;
namespace H.Test.Project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : ApplicationBase
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            
            services.AddAdornerDialogMessage();
            services.AddFormMessageService();
            services.AddProject<UserProjectService>();


            //  Do ：根据登录用户加载不同工程
            //services.AddLoginViewPresenter();
            //services.AddTestLoginService();
            services.AddWindowMessage();
            //services.AddRegisterLoginViewPresenter();
            //services.AddRegisterService();
            //services.AddLoginService();
            //services.AddDbContextBySetting<IdentifyDataContext>();
            //services.AddSingleton<IStringRepository<hi_dd_user>, DbContextRepository<IdentifyDataContext, hi_dd_user>>();
        }

        protected override Window CreateMainWindow(StartupEventArgs e)
        {
            return new MainWindow();
        }
    }
}
