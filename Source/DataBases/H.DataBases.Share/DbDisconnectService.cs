



using System;
using H.Services.Common;
using H.Services.Common.DataBase;



#if NETFRAMEWORK
using System.Data.Entity;
#endif

#if NETCOREAPP
using Microsoft.EntityFrameworkCore;
#endif
namespace H.DataBases.Share
{
    public class DbDisconnectService : IDbDisconnectService
    {
        public string Name => "数据库数据";
        public bool Save(out string message)
        {
            message = null;
            DbContext db = Ioc.GetService<DbContext>();
            db?.SaveChanges();
            //db.Dispose();
            return true;
        }
    }

    public class DbDisconnectService<TDbContext> : IDbDisconnectService where TDbContext : DbContext
    {
        public string Name => "数据库数据";
        public bool Save(out string message)
        {
            message = null;
            TDbContext db = Ioc.GetService<TDbContext>();
            db?.SaveChanges();
            //db.Dispose();
            return true;
        }
    }

}
