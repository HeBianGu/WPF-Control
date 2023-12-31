using Microsoft.EntityFrameworkCore.Migrations;
using System;


namespace H.App.FileManager.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "fm_dd_files",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    Tags = table.Column<string>(type: "TEXT", nullable: true),
                    Extend = table.Column<string>(type: "TEXT", nullable: true),
                    Size = table.Column<long>(type: "INTEGER", nullable: false),
                    PlayCount = table.Column<string>(type: "TEXT", nullable: true),
                    Score = table.Column<string>(type: "TEXT", nullable: true),
                    Favorite = table.Column<bool>(type: "INTEGER", nullable: false),
                    CDATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UDATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ISENBLED = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fm_dd_files", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fm_dd_files");
        }
    }
}
