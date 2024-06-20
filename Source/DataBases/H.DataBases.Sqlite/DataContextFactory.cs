#if NETFRAMEWORK
using System.Data.Entity;
#endif

#if NETCOREAPP
#endif
namespace H.DataBases.Sqlite
{
    //public class DataContextFactory<T> : IDesignTimeDbContextFactory<T> where T : DbContext
    //{
    //    public T CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<T>();
    //        optionsBuilder.UseLazyLoadingProxies().UseSqlite("Data Source=Migration.db");
    //        return new T(optionsBuilder.Options);
    //    }
    //}
}
