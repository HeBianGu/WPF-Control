// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.DataBases.Share;
using Microsoft.EntityFrameworkCore;
using System.IO;
namespace H.DataBases.Sqlite
{
    public class SqliteDbConnectService<TDbContext> : DbConnectServiceBase<TDbContext> where TDbContext : DbContext
    {
        public override string Name => Properties.Resources.DataBase;
        protected override IDbSettable GetSetting()
        {
            return SqliteSettable.Instance;
        }

        public override bool Load(out string message)
        {
            var settings = SqliteSettable.Instance;
            Directory.CreateDirectory(settings.FilePath);
            return base.Load(out message);
        }
    }

    public class SqliteDbDisconnectService<TDbContext> : DbDisconnectService<TDbContext> where TDbContext : DbContext
    {
        public override string Name => Properties.Resources.DataBase;
    }
}
