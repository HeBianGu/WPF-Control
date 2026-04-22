// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using Microsoft.EntityFrameworkCore;

namespace H.App.AIDI.DB;
public class AIDIDataContext : DbContext
{
    public AIDIDataContext(DbContextOptions<AIDIDataContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        RectConverter rectConverter = new RectConverter();
        ColorConverter colorConverter = new ColorConverter();
        modelBuilder.Entity<fm_dd_label>().Property(e => e.BoundingBox)
            .HasConversion(x => rectConverter.ConvertToString(x), x => (Rect)rectConverter.ConvertFromString(x));
        modelBuilder.Entity<fm_dd_label>().Property(e => e.LabelColor)
      .HasConversion(x => colorConverter.ConvertToString(x), x => (Color)ColorConverter.ConvertFromString(x));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    public DbSet<fm_dd_image> fm_dd_images { get; set; }
    public DbSet<fm_dd_label> fm_dd_labels { get; set; }
}




