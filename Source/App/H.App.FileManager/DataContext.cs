using H.Providers.Ioc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace H.App.FileManager
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<fm_dd_file> fm_dd_files { get; set; }
    }
}
