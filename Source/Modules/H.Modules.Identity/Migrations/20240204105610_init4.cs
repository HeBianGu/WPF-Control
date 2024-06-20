using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace H.Modules.Identity.Migrations
{
    /// <inheritdoc />
    public partial class init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "enable",
                table: "hi_dd_users",
                type: "INTEGER",
                nullable: false,
                defaultValue: false)
                .Annotation("Relational:ColumnOrder", 4);

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
                columns: new[] { "CDATE", "enable", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 4, 18, 56, 9, 726, DateTimeKind.Local).AddTicks(8797), false, new DateTime(2024, 2, 4, 18, 56, 9, 726, DateTimeKind.Local).AddTicks(8797) });

            migrationBuilder.UpdateData(
                table: "hi_dd_users",
                keyColumn: "id",
                keyValue: "{E12E19D6-FDD9-4DCE-B211-55E58FAFC207}",
                columns: new[] { "CDATE", "enable", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 4, 18, 56, 9, 726, DateTimeKind.Local).AddTicks(8786), false, new DateTime(2024, 2, 4, 18, 56, 9, 726, DateTimeKind.Local).AddTicks(8786) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "enable",
                table: "hi_dd_users");

            migrationBuilder.UpdateData(
                table: "hi_dd_authors",
                keyColumn: "id",
                keyValue: "{63860D6B-BD59-4620-919D-0DE51877676F}",
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 4, 17, 55, 4, 474, DateTimeKind.Local).AddTicks(267), new DateTime(2024, 2, 4, 17, 55, 4, 474, DateTimeKind.Local).AddTicks(267) });

            migrationBuilder.UpdateData(
                table: "hi_dd_authors",
                keyColumn: "id",
                keyValue: "{DE3B4992-A5BF-4AD2-80D8-2C9654C47A34}",
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 4, 17, 55, 4, 474, DateTimeKind.Local).AddTicks(301), new DateTime(2024, 2, 4, 17, 55, 4, 474, DateTimeKind.Local).AddTicks(301) });

            migrationBuilder.UpdateData(
                table: "hi_dd_roles",
                keyColumn: "id",
                keyValue: "{0E465AF1-4C5B-417B-B496-E74E8A0D7E5C}",
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 4, 17, 55, 4, 474, DateTimeKind.Local).AddTicks(360), new DateTime(2024, 2, 4, 17, 55, 4, 474, DateTimeKind.Local).AddTicks(360) });

            migrationBuilder.UpdateData(
                table: "hi_dd_roles",
                keyColumn: "id",
                keyValue: "{4360CE12-E5F4-4EA6-937C-9FDEA4DF06F6}",
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 4, 17, 55, 4, 474, DateTimeKind.Local).AddTicks(337), new DateTime(2024, 2, 4, 17, 55, 4, 474, DateTimeKind.Local).AddTicks(337) });

            migrationBuilder.UpdateData(
                table: "hi_dd_users",
                keyColumn: "id",
                keyValue: "{A8EE1331-0DA7-42F1-80E2-CD2A20D62BC9}",
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 4, 17, 55, 4, 474, DateTimeKind.Local).AddTicks(381), new DateTime(2024, 2, 4, 17, 55, 4, 474, DateTimeKind.Local).AddTicks(381) });

            migrationBuilder.UpdateData(
                table: "hi_dd_users",
                keyColumn: "id",
                keyValue: "{E12E19D6-FDD9-4DCE-B211-55E58FAFC207}",
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 4, 17, 55, 4, 474, DateTimeKind.Local).AddTicks(367), new DateTime(2024, 2, 4, 17, 55, 4, 474, DateTimeKind.Local).AddTicks(367) });
        }
    }
}
