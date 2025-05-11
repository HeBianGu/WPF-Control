// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

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

