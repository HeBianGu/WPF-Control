// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.DataBases.SqlServer;

//
#if NETFRAMEWORK
using System.Data.Entity;
#endif

#if NETCOREAPP
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
