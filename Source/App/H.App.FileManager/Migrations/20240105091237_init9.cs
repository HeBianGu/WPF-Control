using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace H.App.FileManager.Migrations
{
    /// <inheritdoc />
    public partial class init9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PixelFormat",
                table: "fm_dd_files",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PixelFormat",
                table: "fm_dd_files");
        }
    }
}
