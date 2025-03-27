using H.DataBases.SqlServer;


//
#if NETFRAMEWORK
using System.Data.Entity;
#endif

#if NETCOREAPP
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using H.Services.Common;
using H.DataBases.Share;
using H.Services.Common.DataBase;
using H.Services.Setting;


#endif

namespace System
{
    public static class SqlServerExtention
    {
        ///// 注册
        ///// </summary>
        ///// <param name="service"></param>
        //public static void AddDbConnectService<TDbContext>(this IServiceCollection service) where TDbContext : DbContext
        //{
        //    service.AddSingleton<IDbConnectService, SqliteDbConnectService<TDbContext>>();
        //    service.AddSingleton<IDbDisconnectService, DbDisconnectService<TDbContext>>();
        //}

        public static void AddDbContextBySetting<TDbContext>(this IServiceCollection services, Action<ISqlServerOption> action = null) where TDbContext : DbContext
        {
            action?.Invoke(SqlServerSettable.Instance);
            SqlServerSettable.Instance.Load(out string message);
            string connect = SqlServerSettable.Instance.GetConnect();
            services.AddDbContext<TDbContext>(x => x.UseSqlServer(connect));
            //IocSetting.Instance.Add(SqlServerSettable.Instance);
            services.AddSingleton<IDbConnectService, SqlServerDbConnectService<TDbContext>>();
            services.AddSingleton<IDbDisconnectService, DbDisconnectService<TDbContext>>();
        }

        public static void UseSqlServer(this IApplicationBuilder service, Action<ISqlServerOption> action = null)
        {
            action?.Invoke(SqlServerSettable.Instance);
            IocSetting.Instance.Add(SqlServerSettable.Instance);
        }
    }
}
