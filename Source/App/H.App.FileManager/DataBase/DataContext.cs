using Microsoft.EntityFrameworkCore;

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
        public DbSet<fm_dd_image> fm_dd_images { get; set; }
        public DbSet<fm_dd_video> fm_dd_videos { get; set; }
        public DbSet<fm_dd_audio> fm_dd_audios { get; set; }
    }
}
