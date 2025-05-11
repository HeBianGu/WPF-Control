// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

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

