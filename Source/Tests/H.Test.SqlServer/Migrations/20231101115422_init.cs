using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace H.Test.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mbc_dv_images",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    MediaType = table.Column<string>(type: "TEXT", nullable: true),
                    CaseType = table.Column<string>(type: "TEXT", nullable: true),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    TagTypes = table.Column<string>(type: "TEXT", nullable: true),
                    AreaType = table.Column<string>(type: "TEXT", nullable: true),
                    ExtendType = table.Column<string>(type: "TEXT", nullable: true),
                    ArticulationType = table.Column<string>(type: "TEXT", nullable: true),
                    Size = table.Column<long>(type: "INTEGER", nullable: false),
                    VipType = table.Column<string>(type: "TEXT", nullable: true),
                    FromType = table.Column<string>(type: "TEXT", nullable: true),
                    OrderNum = table.Column<string>(type: "TEXT", nullable: true),
                    PlayCount = table.Column<string>(type: "TEXT", nullable: true),
                    Score = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    Resoluction = table.Column<string>(type: "TEXT", nullable: true),
                    Aspect = table.Column<string>(type: "TEXT", nullable: true),
                    CDATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UDATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ISENBLED = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mbc_dv_images", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mbc_dv_images");
        }
    }
}
