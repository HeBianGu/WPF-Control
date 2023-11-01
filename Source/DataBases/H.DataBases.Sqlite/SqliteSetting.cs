


#if NETFRAMEWORK
using System.Data.Entity;
#endif

#if NETCOREAPP
#endif
using System.ComponentModel.DataAnnotations;
using H.Providers.Ioc;
using H.DataBases.Share;

namespace H.DataBases.Sqlite
{
    [Display(Name = "数据库配置", GroupName = SystemSetting.GroupApp)]
    public class SqliteSetting : SqliteSettingBase<SqliteSetting>, ISqliteOption, IDbSetting
    {

    }
}
