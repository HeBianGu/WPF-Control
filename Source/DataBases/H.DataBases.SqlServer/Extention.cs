using H.DataBases.SqlServer;


//
#if NETFRAMEWORK
using System.Data.Entity;
#endif

#if NETCOREAPP
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using H.Providers.Ioc;
using H.DataBases.Share;


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
            action?.Invoke(SqlServerSetting.Instance);
            SqlServerSetting.Instance.Load();
            string connect = SqlServerSetting.Instance.GetConnect();
            services.AddDbContext<TDbContext>(x => x.UseSqlServer(connect));
            SettingDataManager.Instance.Add(SqlServerSetting.Instance);
            services.AddSingleton<IDbConnectService, SqlServerDbConnectService<TDbContext>>();
            services.AddSingleton<IDbDisconnectService, DbDisconnectService<TDbContext>>();
        }
    }
}
