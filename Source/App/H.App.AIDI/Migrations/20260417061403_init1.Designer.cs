// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.App.AIDI.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

#nullable disable

namespace H.App.AIDI.Migrations;
[DbContext(typeof(AIDIDataContext))]
[Migration("20260417061403_init1")]
partial class init1
{
    /// <inheritdoc />
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.16")
            .HasAnnotation("Proxies:ChangeTracking", false)
            .HasAnnotation("Proxies:CheckEquality", false)
            .HasAnnotation("Proxies:LazyLoading", true);

        modelBuilder.Entity("H.App.AIDI.Model.fm_dd_image", b =>
            {
                b.Property<string>("ID")
                    .HasColumnType("TEXT")
                    .HasColumnName("id")
                    .HasColumnOrder(0);

                b.Property<DateTime>("CDATE")
                    .HasColumnType("TEXT");

                b.Property<string>("Extend")
                    .HasColumnType("TEXT");

                b.Property<bool>("Favorite")
                    .HasColumnType("INTEGER");

                b.Property<string>("FavoritePath")
                    .HasColumnType("TEXT");

                b.Property<int>("ISENBLED")
                    .HasColumnType("INTEGER");

                b.Property<string>("Introduction")
                    .HasColumnType("TEXT");

                b.Property<DateTime>("LastPlayTime")
                    .HasColumnType("TEXT");

                b.Property<string>("ModuleID")
                    .HasColumnType("TEXT");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.Property<int>("PixelHeight")
                    .HasColumnType("INTEGER");

                b.Property<int>("PixelWidth")
                    .HasColumnType("INTEGER");

                b.Property<int>("PlayCount")
                    .HasColumnType("INTEGER");

                b.Property<string>("ProjectID")
                    .HasColumnType("TEXT");

                b.Property<int>("Score")
                    .HasColumnType("INTEGER");

                b.Property<bool>("SeeLater")
                    .HasColumnType("INTEGER");

                b.Property<long>("Size")
                    .HasColumnType("INTEGER");

                b.Property<string>("Tags")
                    .HasColumnType("TEXT");

                b.Property<string>("Type")
                    .HasColumnType("TEXT");

                b.Property<DateTime>("UDATE")
                    .HasColumnType("TEXT");

                b.Property<string>("Url")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.Property<bool>("Watched")
                    .HasColumnType("INTEGER");

                b.HasKey("ID");

                b.ToTable("fm_dd_images");
            });

        modelBuilder.Entity("H.App.AIDI.Model.fm_dd_label", b =>
            {
                b.Property<string>("ID")
                    .HasColumnType("TEXT")
                    .HasColumnName("id")
                    .HasColumnOrder(0);

                b.Property<string>("BoundingBox")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.Property<DateTime>("CDATE")
                    .HasColumnType("TEXT");

                b.Property<int>("ISENBLED")
                    .HasColumnType("INTEGER");

                b.Property<string>("ImageID")
                    .HasColumnType("TEXT");

                b.Property<string>("LabelColor")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.Property<string>("LabelDescription")
                    .HasColumnType("TEXT");

                b.Property<string>("LabelName")
                    .HasColumnType("TEXT");

                b.Property<DateTime>("UDATE")
                    .HasColumnType("TEXT");

                b.HasKey("ID");

                b.HasIndex("ImageID");

                b.ToTable("fm_dd_labels");
            });

        modelBuilder.Entity("H.App.AIDI.Model.fm_dd_label", b =>
            {
                b.HasOne("H.App.AIDI.Model.fm_dd_image", "Image")
                    .WithMany("Labels")
                    .HasForeignKey("ImageID");

                b.Navigation("Image");
            });

        modelBuilder.Entity("H.App.AIDI.Model.fm_dd_image", b =>
            {
                b.Navigation("Labels");
            });
#pragma warning restore 612, 618
    }
}
