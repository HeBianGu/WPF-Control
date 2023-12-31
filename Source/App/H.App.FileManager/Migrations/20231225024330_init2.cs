using Microsoft.EntityFrameworkCore.Migrations;


namespace H.App.FileManager.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "fm_dd_files",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PixelHeight",
                table: "fm_dd_files",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PixelWidth",
                table: "fm_dd_files",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "fm_dd_files");

            migrationBuilder.DropColumn(
                name: "PixelHeight",
                table: "fm_dd_files");

            migrationBuilder.DropColumn(
                name: "PixelWidth",
                table: "fm_dd_files");
        }
    }
}
