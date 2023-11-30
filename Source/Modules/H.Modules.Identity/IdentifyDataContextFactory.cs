using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace H.Modules.Identity
{
    public class IdentifyDataContextFactory : IDesignTimeDbContextFactory<IdentifyDataContext>
    {
        public IdentifyDataContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<IdentifyDataContext> optionsBuilder = new DbContextOptionsBuilder<IdentifyDataContext>();
            optionsBuilder.UseLazyLoadingProxies().UseSqlite("Data Source=Migration.db");
            return new IdentifyDataContext(optionsBuilder.Options);
        }
    }
}


