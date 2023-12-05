using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace H.Modules.Operation
{
    public class OperationDataContextFactory : IDesignTimeDbContextFactory<OperationDataContext>
    {
        public OperationDataContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<OperationDataContext> optionsBuilder = new DbContextOptionsBuilder<OperationDataContext>();
            optionsBuilder.UseLazyLoadingProxies().UseSqlite("Data Source=Migration.db");
            return new OperationDataContext(optionsBuilder.Options);
        }
    }
}


