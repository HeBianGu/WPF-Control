using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace H.Modules.Identity.Migrations
{
    /// <inheritdoc />
    public partial class init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "role_id",
                table: "hi_dd_users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_login_time",
                table: "hi_dd_users",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.AddColumn<DateTime>(
                name: "license_deadline",
                table: "hi_dd_users",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .Annotation("Relational:ColumnOrder", 7);

            migrationBuilder.UpdateData(
                table: "hi_dd_authors",
                keyColumn: "id",
                keyValue: "{63860D6B-BD59-4620-919D-0DE51877676F}",
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 5, 14, 31, 20, 540, DateTimeKind.Local).AddTicks(9929), new DateTime(2024, 2, 5, 14, 31, 20, 540, DateTimeKind.Local).AddTicks(9929) });

            migrationBuilder.UpdateData(
                table: "hi_dd_authors",
                keyColumn: "id",
                keyValue: "{DE3B4992-A5BF-4AD2-80D8-2C9654C47A34}",
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 5, 14, 31, 20, 540, DateTimeKind.Local).AddTicks(9992), new DateTime(2024, 2, 5, 14, 31, 20, 540, DateTimeKind.Local).AddTicks(9992) });

            migrationBuilder.UpdateData(
                table: "hi_dd_roles",
                keyColumn: "id",
                keyValue: "{0E465AF1-4C5B-417B-B496-E74E8A0D7E5C}",
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 5, 14, 31, 20, 541, DateTimeKind.Local).AddTicks(59), new DateTime(2024, 2, 5, 14, 31, 20, 541, DateTimeKind.Local).AddTicks(59) });

            migrationBuilder.UpdateData(
                table: "hi_dd_roles",
                keyColumn: "id",
                keyValue: "{4360CE12-E5F4-4EA6-937C-9FDEA4DF06F6}",
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 5, 14, 31, 20, 541, DateTimeKind.Local).AddTicks(36), new DateTime(2024, 2, 5, 14, 31, 20, 541, DateTimeKind.Local).AddTicks(36) });

            migrationBuilder.UpdateData(
                table: "hi_dd_users",
                keyColumn: "id",
                keyValue: "{A8EE1331-0DA7-42F1-80E2-CD2A20D62BC9}",
                columns: new[] { "CDATE", "last_login_time", "license_deadline", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 5, 14, 31, 20, 541, DateTimeKind.Local).AddTicks(86), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 5, 14, 31, 20, 541, DateTimeKind.Local).AddTicks(86) });

            migrationBuilder.UpdateData(
                table: "hi_dd_users",
                keyColumn: "id",
                keyValue: "{E12E19D6-FDD9-4DCE-B211-55E58FAFC207}",
                columns: new[] { "CDATE", "last_login_time", "license_deadline", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 5, 14, 31, 20, 541, DateTimeKind.Local).AddTicks(70), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 5, 14, 31, 20, 541, DateTimeKind.Local).AddTicks(70) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_login_time",
                table: "hi_dd_users");

            migrationBuilder.DropColumn(
                name: "license_deadline",
                table: "hi_dd_users");

            migrationBuilder.AlterColumn<string>(
                name: "role_id",
                table: "hi_dd_users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.UpdateData(
                table: "hi_dd_authors",
                keyColumn: "id",
                keyValue: "{63860D6B-BD59-4620-919D-0DE51877676F}",
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 4, 18, 56, 9, 726, DateTimeKind.Local).AddTicks(8715), new DateTime(2024, 2, 4, 18, 56, 9, 726, DateTimeKind.Local).AddTicks(8715) });

            migrationBuilder.UpdateData(
                table: "hi_dd_authors",
                keyColumn: "id",
                keyValue: "{DE3B4992-A5BF-4AD2-80D8-2C9654C47A34}",
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 4, 18, 56, 9, 726, DateTimeKind.Local).AddTicks(8752), new DateTime(2024, 2, 4, 18, 56, 9, 726, DateTimeKind.Local).AddTicks(8752) });

            migrationBuilder.UpdateData(
                table: "hi_dd_roles",
                keyColumn: "id",
                keyValue: "{0E465AF1-4C5B-417B-B496-E74E8A0D7E5C}",
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 4, 18, 56, 9, 726, DateTimeKind.Local).AddTicks(8777), new DateTime(2024, 2, 4, 18, 56, 9, 726, DateTimeKind.Local).AddTicks(8777) });

            migrationBuilder.UpdateData(
                table: "hi_dd_roles",
                keyColumn: "id",
                keyValue: "{4360CE12-E5F4-4EA6-937C-9FDEA4DF06F6}",
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 4, 18, 56, 9, 726, DateTimeKind.Local).AddTicks(8767), new DateTime(2024, 2, 4, 18, 56, 9, 726, DateTimeKind.Local).AddTicks(8767) });

            migrationBuilder.UpdateData(
                table: "hi_dd_users",
                keyColumn: "id",
                keyValue: "{A8EE1331-0DA7-42F1-80E2-CD2A20D62BC9}",
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 4, 18, 56, 9, 726, DateTimeKind.Local).AddTicks(8797), new DateTime(2024, 2, 4, 18, 56, 9, 726, DateTimeKind.Local).AddTicks(8797) });

            migrationBuilder.UpdateData(
                table: "hi_dd_users",
                keyColumn: "id",
                keyValue: "{E12E19D6-FDD9-4DCE-B211-55E58FAFC207}",
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 4, 18, 56, 9, 726, DateTimeKind.Local).AddTicks(8786), new DateTime(2024, 2, 4, 18, 56, 9, 726, DateTimeKind.Local).AddTicks(8786) });
        }
    }
}
