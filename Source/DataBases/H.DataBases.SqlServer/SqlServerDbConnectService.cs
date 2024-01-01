using H.DataBases.Share;

#if NETFRAMEWORK
using System.Data.Entity;
#endif

#if NETCOREAPP
using Microsoft.EntityFrameworkCore;
#endif
namespace H.DataBases.SqlServer
{
    public class SqlServerDbConnectService<TDbContext> : DbConnectServiceBase<TDbContext> where TDbContext : DbContext
    {
        protected override IDbSettable GetSetting()
        {
            return SqlServerSettable.Instance;
        }

    }
}
