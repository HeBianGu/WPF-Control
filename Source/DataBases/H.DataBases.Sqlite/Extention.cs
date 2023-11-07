using H.DataBases.Sqlite;



#if NETFRAMEWORK
using System.Data.Entity;
#endif

#if NETCOREAPP
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using H.Providers.Ioc;
using H.DataBases.Share;
#endif

namespace System
{
    public static class SqliteExtention
    {
        ///// 注册
        ///// </summary>
        ///// <param name="service"></param>
        //public static void AddDbConnectService<TDbContext>(this IServiceCollection service) where TDbContext : DbContext
        //{
        //    service.AddSingleton<IDbConnectService, SqliteDbConnectService<TDbContext>>();
        //    service.AddSingleton<IDbDisconnectService, DbDisconnectService<TDbContext>>();
        //}

        public static void AddDbContextBySetting<TDbContext>(this IServiceCollection services, Action<ISqliteOption> action = null) where TDbContext : DbContext
        {
            action?.Invoke(SqliteSetting.Instance);
            SqliteSetting.Instance.Load();
            string connect = SqliteSetting.Instance.GetConnect();
            services.AddDbContext<TDbContext>(x => x.UseSqlite(connect));
            SettingDataManager.Instance.Add(SqliteSetting.Instance);
            services.AddSingleton<IDbConnectService, SqliteDbConnectService<TDbContext>>();
            services.AddSingleton<IDbDisconnectService, DbDisconnectService<TDbContext>>();
        }

    }
}
