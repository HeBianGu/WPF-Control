



using System;
using System.Windows.Input;
using H.Providers.Ioc;


#if NETFRAMEWORK
using System.Data.Entity;
#endif

#if NETCOREAPP
using Microsoft.EntityFrameworkCore;
#endif
namespace H.DataBases.Share
{
    public abstract class DbConnectServiceBase<TDbContext> : IDbConnectService where TDbContext : DbContext
    {
        public string Name => "数据库";
        protected abstract IDbSetting GetSetting();
        protected virtual bool CanConnect(DbContext db, out string message)
        {
            message = null;
#if NETCOREAPP
            try
            {
                //db.Database.EnsureCreated();
                db.Database.Migrate();
                return db.Database.CanConnect();
            }
            catch (System.Exception ex)
            {
                Logger.Instance?.Error(ex);
                message = ex.Message;
                return false;
            }
#endif

#if NETFRAMEWORK
           return db.Database.Exists();
#endif
        }

        public virtual bool Load(out string message)
        {
            message = null;
            var context = Ioc.GetService<TDbContext>();
            bool r = this.CanConnect(context, out message);
            if (!r)
            {
                var result = IocMessage.Window.ShowMessage(message, "数据库连接失败，是否重新配置?").Result;
                if (result != true)
                    return false;
                result = IocMessage.Window.Show(this.GetSetting(), null, DialogButton.None, "数据库连接失败，请重新配置数据库").Result;
                message = "数据库配置已修改，请重新启动";
                IocMessage.Window.ShowMessage("数据库配置已修改，请重新启动").Wait();
                return false;
            }


#if NETCOREAPP
            //db.Database.EnsureCreated();
#endif

#if NETFRAMEWORK
            db.Database.CreateIfNotExists();
#endif
            return true;
        }
        public bool TryConnect(out string message)
        {
            message = null;
            try
            {
                var context = Ioc.GetService<TDbContext>();
                string connect = this.GetSetting().GetConnect();
                context.Database.SetConnectionString(connect);
                context.Database.Migrate();
                var r = context.Database.CanConnect();
                message = r ? "连接成功" : "连接失败";
                CommandManager.InvalidateRequerySuggested();
                return r;
                //#endif
            }
            catch (Exception ex)
            {
                Logger.Instance?.Error(ex);
                message = ex.Message;
                return false;
            }
        }
    }

}
