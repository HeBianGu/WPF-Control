using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace H.Modules.Operation
{
    public class OperationDataContext : DbContext
    {
        public OperationDataContext(DbContextOptions<OperationDataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<hi_dd_operation> hi_dd_operations { get; set; }
    }
}


