using Microsoft.EntityFrameworkCore;
using System;

namespace H.Modules.Identity
{
    public class IdentifyDataContext : DbContext
    {
        public IdentifyDataContext(DbContextOptions<IdentifyDataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.BuildIdentifySeed();
        }

        public DbSet<hi_dd_user> hi_dd_users { get; set; }
        public DbSet<hi_dd_role> hi_dd_roles { get; set; }
        public DbSet<hi_dd_author> hi_dd_authors { get; set; }

    }
}


