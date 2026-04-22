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
public partial class init2 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "ModuleID",
            table: "fm_dd_images",
            newName: "PageID");

        migrationBuilder.AddColumn<int>(
            name: "DatasetType",
            table: "fm_dd_images",
            type: "INTEGER",
            nullable: false,
            defaultValue: 0);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "DatasetType",
            table: "fm_dd_images");

        migrationBuilder.RenameColumn(
            name: "PageID",
            table: "fm_dd_images",
            newName: "ModuleID");
    }
}
