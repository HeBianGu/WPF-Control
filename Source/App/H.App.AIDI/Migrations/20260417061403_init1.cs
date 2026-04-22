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
public partial class init1 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "ModuleID",
            table: "fm_dd_images",
            type: "TEXT",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "ProjectID",
            table: "fm_dd_images",
            type: "TEXT",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "ModuleID",
            table: "fm_dd_images");

        migrationBuilder.DropColumn(
            name: "ProjectID",
            table: "fm_dd_images");
    }
}
