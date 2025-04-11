
using H.ApplicationBases.Identify;
using H.DataBases.Share;
using H.DataBases.Sqlite;
using H.Extensions.DataBase;
using H.Modules.Identity;
using H.Modules.Login;
using H.Modules.Operation;
using H.Services.Common.About;
using Microsoft.Extensions.DependencyInjection;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddIdentifyDefaultServices(this IServiceCollection services, Action<IDefaultIndentifyOptions> options = null)
        {
            DefaultIndentifyOptions opt = new DefaultIndentifyOptions();
            options?.Invoke(opt);
            //  Do ：身份认证
            services.AddDbContextBySetting<IdentifyDataContext>(opt.GetConfigOptions<Action<ISqliteSettable>>());
            services.AddSingleton<IStringRepository<hi_dd_user>, DbContextRepository<IdentifyDataContext, hi_dd_user>>();
            services.AddUserViewPresenter();

            services.AddSingleton<IStringRepository<hi_dd_role>, DbContextRepository<IdentifyDataContext, hi_dd_role>>();
            services.AddRoleViewPresenter();

            services.AddSingleton<IStringRepository<hi_dd_author>, DbContextRepository<IdentifyDataContext, hi_dd_author>>();
            services.AddAuthorityViewPresenter();

            //  Do ：操作日志
            services.AddDbContextBySetting<OperationDataContext>(opt.GetConfigOptions<Action<ISqliteSettable>>());
            services.AddSingleton<IStringRepository<hi_dd_operation>, DbContextRepository<OperationDataContext, hi_dd_operation>>();
            services.AddOperationViewPresenter();

            //  Do ：登录和注册页面
            services.AddRegisterLoginViewPresenter(opt.GetConfigOptions<Action<ILoginOptions>>(), opt.GetConfigOptions<Action<IRegistorOptions>>());
            services.AddIdentityLoginService();
            services.AddIdentityRegisterService(opt.GetConfigOptions<Action<IIdentifyOptions>>());
        }


        public static void UseIdentifyDefaultOptions(this IApplicationBuilder app, Action<IDefaultIndentifyOptions> options = null)
        {
            DefaultIndentifyOptions opt = new DefaultIndentifyOptions();
            options?.Invoke(opt);
            app.UseLoginOptions(opt.GetConfigOptions<Action<ILoginOptions>>());
            app.UseRegistorOptions(opt.GetConfigOptions<Action<IRegistorOptions>>());
            app.UseSqlite();
        }
    }
}
