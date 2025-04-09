
using H.DataBases.Share;
using H.Extensions.DataBase;
using H.Modules.Identity;
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
        public static void AddDefaultIdentify(this IServiceCollection services)
        {
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
        }


        public static void UseDefaultIdentifyOptions(this IApplicationBuilder app)
        {
            app.UseLoginOptions();
            app.UseRegistorOptions(x => x.UseMail = false);
            app.UseSqlite();
        }
    }
}
