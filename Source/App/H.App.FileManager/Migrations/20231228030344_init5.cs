using Microsoft.EntityFrameworkCore.Migrations;


namespace H.App.FileManager.Migrations
{
    /// <inheritdoc />
    public partial class init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SelectedImageIndex",
                table: "fm_dd_files",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TimeStamp",
                table: "fm_dd_files",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedImageIndex",
                table: "fm_dd_files");

            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "fm_dd_files");
        }
    }
}
