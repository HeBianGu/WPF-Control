


#if NETFRAMEWORK
using System.Data.Entity;
#endif

#if NETCOREAPP
#endif
using H.DataBases.Share;
using H.Services.Common;
using H.Services.Setting;
using System.ComponentModel.DataAnnotations;

namespace H.DataBases.Sqlite
{
    [Display(Name = "数据库配置", GroupName = SettingGroupNames.GroupApp)]
    public class SqliteSettable : SqliteSettableBase<SqliteSettable>, ISqliteOption, IDbSettable
    {

    }
}
