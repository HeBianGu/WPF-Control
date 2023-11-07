using H.DataBases.Share;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace H.Test.RepositoryPresenter
{
    public class MyDataContext : DbContext
    {
        public MyDataContext(DbContextOptions<MyDataContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<mbc_dv_image> mbc_dv_images { get; set; }

    }

    public class MyDataContextFactory : IDesignTimeDbContextFactory<MyDataContext>
    {
        public MyDataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyDataContext>();
            optionsBuilder.UseLazyLoadingProxies().UseSqlite("Data Source=Migration.db");
            return new MyDataContext(optionsBuilder.Options);
        }
    }
}
