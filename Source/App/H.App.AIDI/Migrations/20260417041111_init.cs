// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace H.App.AIDI.Migrations;
/// <inheritdoc />
public partial class init : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "fm_dd_images",
            columns: table => new
            {
                id = table.Column<string>(type: "TEXT", nullable: false),
                PixelWidth = table.Column<int>(type: "INTEGER", nullable: false),
                PixelHeight = table.Column<int>(type: "INTEGER", nullable: false),
                Introduction = table.Column<string>(type: "TEXT", nullable: true),
                CDATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                UDATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                ISENBLED = table.Column<int>(type: "INTEGER", nullable: false),
                Name = table.Column<string>(type: "TEXT", nullable: false),
                Type = table.Column<string>(type: "TEXT", nullable: true),
                Url = table.Column<string>(type: "TEXT", nullable: false),
                Tags = table.Column<string>(type: "TEXT", nullable: true),
                Extend = table.Column<string>(type: "TEXT", nullable: true),
                Size = table.Column<long>(type: "INTEGER", nullable: false),
                PlayCount = table.Column<int>(type: "INTEGER", nullable: false),
                Score = table.Column<int>(type: "INTEGER", nullable: false),
                Favorite = table.Column<bool>(type: "INTEGER", nullable: false),
                FavoritePath = table.Column<string>(type: "TEXT", nullable: true),
                LastPlayTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                Watched = table.Column<bool>(type: "INTEGER", nullable: false),
                SeeLater = table.Column<bool>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_fm_dd_images", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "fm_dd_labels",
            columns: table => new
            {
                id = table.Column<string>(type: "TEXT", nullable: false),
                LabelName = table.Column<string>(type: "TEXT", nullable: true),
                LabelColor = table.Column<string>(type: "TEXT", nullable: false),
                LabelDescription = table.Column<string>(type: "TEXT", nullable: true),
                BoundingBox = table.Column<string>(type: "TEXT", nullable: false),
                ImageID = table.Column<string>(type: "TEXT", nullable: true),
                CDATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                UDATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                ISENBLED = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_fm_dd_labels", x => x.id);
                table.ForeignKey(
                    name: "FK_fm_dd_labels_fm_dd_images_ImageID",
                    column: x => x.ImageID,
                    principalTable: "fm_dd_images",
                    principalColumn: "id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_fm_dd_labels_ImageID",
            table: "fm_dd_labels",
            column: "ImageID");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "fm_dd_labels");

        migrationBuilder.DropTable(
            name: "fm_dd_images");
    }
}
