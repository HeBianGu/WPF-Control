// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

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

