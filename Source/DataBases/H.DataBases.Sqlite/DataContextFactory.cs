

#if NETFRAMEWORK
using System.Data.Entity;
#endif

#if NETCOREAPP
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using H.DataBases.Share;
#endif
namespace H.DataBases.Sqlite
{
//#if NETCOREAPP
//    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
//    {
//        public DataContext CreateDbContext(string[] args)
//        {
//            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
//            optionsBuilder.UseLazyLoadingProxies().UseSqlite("Data Source=Migration.db");
//            return new DataContext(optionsBuilder.Options);
//        }
//    }
//#endif
}
