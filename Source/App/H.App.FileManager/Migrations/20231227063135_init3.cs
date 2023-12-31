using Microsoft.EntityFrameworkCore.Migrations;
using System;


namespace H.App.FileManager.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Project",
                table: "fm_dd_files",
                newName: "fm_dd_videoID");

            migrationBuilder.AlterColumn<int>(
                name: "Score",
                table: "fm_dd_files",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "fm_dd_files",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Articulation",
                table: "fm_dd_files",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bitrate",
                table: "fm_dd_files",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Duration",
                table: "fm_dd_files",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "fm_dd_files",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Introduction",
                table: "fm_dd_files",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastPlayTime",
                table: "fm_dd_files",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "LastPlayTimeStamp",
                table: "fm_dd_files",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Object",
                table: "fm_dd_files",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rate",
                table: "fm_dd_files",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Torrent",
                table: "fm_dd_files",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoCode",
                table: "fm_dd_files",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Watched",
                table: "fm_dd_files",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Year",
                table: "fm_dd_files",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_fm_dd_files_fm_dd_videoID",
                table: "fm_dd_files",
                column: "fm_dd_videoID");

            migrationBuilder.AddForeignKey(
                name: "FK_fm_dd_files_fm_dd_files_fm_dd_videoID",
                table: "fm_dd_files",
                column: "fm_dd_videoID",
                principalTable: "fm_dd_files",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_fm_dd_files_fm_dd_files_fm_dd_videoID",
                table: "fm_dd_files");

            migrationBuilder.DropIndex(
                name: "IX_fm_dd_files_fm_dd_videoID",
                table: "fm_dd_files");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "fm_dd_files");

            migrationBuilder.DropColumn(
                name: "Articulation",
                table: "fm_dd_files");

            migrationBuilder.DropColumn(
                name: "Bitrate",
                table: "fm_dd_files");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "fm_dd_files");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "fm_dd_files");

            migrationBuilder.DropColumn(
                name: "Introduction",
                table: "fm_dd_files");

            migrationBuilder.DropColumn(
                name: "LastPlayTime",
                table: "fm_dd_files");

            migrationBuilder.DropColumn(
                name: "LastPlayTimeStamp",
                table: "fm_dd_files");

            migrationBuilder.DropColumn(
                name: "Object",
                table: "fm_dd_files");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "fm_dd_files");

            migrationBuilder.DropColumn(
                name: "Torrent",
                table: "fm_dd_files");

            migrationBuilder.DropColumn(
                name: "VideoCode",
                table: "fm_dd_files");

            migrationBuilder.DropColumn(
                name: "Watched",
                table: "fm_dd_files");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "fm_dd_files");

            migrationBuilder.RenameColumn(
                name: "fm_dd_videoID",
                table: "fm_dd_files",
                newName: "Project");

            migrationBuilder.AlterColumn<string>(
                name: "Score",
                table: "fm_dd_files",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
