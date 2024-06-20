using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace H.Modules.Identity.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "mail",
                table: "hi_dd_users",
                type: "TEXT",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.UpdateData(
                table: "hi_dd_authors",
                keyColumn: "id",
                keyValue: "{63860D6B-BD59-4620-919D-0DE51877676F}",
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 2, 19, 18, 56, 642, DateTimeKind.Local).AddTicks(9421), new DateTime(2024, 2, 2, 19, 18, 56, 642, DateTimeKind.Local).AddTicks(9421) });

            migrationBuilder.UpdateData(
                table: "hi_dd_authors",
                keyColumn: "id",
                keyValue: "{DE3B4992-A5BF-4AD2-80D8-2C9654C47A34}",
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 2, 19, 18, 56, 642, DateTimeKind.Local).AddTicks(9450), new DateTime(2024, 2, 2, 19, 18, 56, 642, DateTimeKind.Local).AddTicks(9450) });

            migrationBuilder.UpdateData(
                table: "hi_dd_roles",
                keyColumn: "id",
                keyValue: "{0E465AF1-4C5B-417B-B496-E74E8A0D7E5C}",
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 2, 19, 18, 56, 642, DateTimeKind.Local).AddTicks(9473), new DateTime(2024, 2, 2, 19, 18, 56, 642, DateTimeKind.Local).AddTicks(9473) });

            migrationBuilder.UpdateData(
                table: "hi_dd_roles",
                keyColumn: "id",
                keyValue: "{4360CE12-E5F4-4EA6-937C-9FDEA4DF06F6}",
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 2, 19, 18, 56, 642, DateTimeKind.Local).AddTicks(9464), new DateTime(2024, 2, 2, 19, 18, 56, 642, DateTimeKind.Local).AddTicks(9464) });

            migrationBuilder.UpdateData(
                table: "hi_dd_users",
                keyColumn: "id",
                keyValue: "{A8EE1331-0DA7-42F1-80E2-CD2A20D62BC9}",
                columns: new[] { "account", "CDATE", "mail", "UDATE" },
                values: new object[] { "user", new DateTime(2024, 2, 2, 19, 18, 56, 642, DateTimeKind.Local).AddTicks(9503), null, new DateTime(2024, 2, 2, 19, 18, 56, 642, DateTimeKind.Local).AddTicks(9503) });

            migrationBuilder.UpdateData(
                table: "hi_dd_users",
                keyColumn: "id",
                keyValue: "{E12E19D6-FDD9-4DCE-B211-55E58FAFC207}",
                columns: new[] { "CDATE", "mail", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 2, 19, 18, 56, 642, DateTimeKind.Local).AddTicks(9480), null, new DateTime(2024, 2, 2, 19, 18, 56, 642, DateTimeKind.Local).AddTicks(9480) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "mail",
                table: "hi_dd_users");

            migrationBuilder.UpdateData(
                table: "hi_dd_authors",
                keyColumn: "id",
                keyValue: "{63860D6B-BD59-4620-919D-0DE51877676F}",
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2023, 11, 14, 17, 43, 38, 515, DateTimeKind.Local).AddTicks(257), new DateTime(2023, 11, 14, 17, 43, 38, 515, DateTimeKind.Local).AddTicks(257) });

            migrationBuilder.UpdateData(
                table: "hi_dd_authors",
                keyColumn: "id",
                keyValue: "{DE3B4992-A5BF-4AD2-80D8-2C9654C47A34}",
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2023, 11, 14, 17, 43, 38, 515, DateTimeKind.Local).AddTicks(290), new DateTime(2023, 11, 14, 17, 43, 38, 515, DateTimeKind.Local).AddTicks(290) });

            migrationBuilder.UpdateData(
                table: "hi_dd_roles",
                keyColumn: "id",
                keyValue: "{0E465AF1-4C5B-417B-B496-E74E8A0D7E5C}",
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2023, 11, 14, 17, 43, 38, 515, DateTimeKind.Local).AddTicks(302), new DateTime(2023, 11, 14, 17, 43, 38, 515, DateTimeKind.Local).AddTicks(302) });

            migrationBuilder.UpdateData(
                table: "hi_dd_roles",
                keyColumn: "id",
                keyValue: "{4360CE12-E5F4-4EA6-937C-9FDEA4DF06F6}",
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2023, 11, 14, 17, 43, 38, 515, DateTimeKind.Local).AddTicks(297), new DateTime(2023, 11, 14, 17, 43, 38, 515, DateTimeKind.Local).AddTicks(297) });

            migrationBuilder.UpdateData(
                table: "hi_dd_users",
                keyColumn: "id",
                keyValue: "{A8EE1331-0DA7-42F1-80E2-CD2A20D62BC9}",
                columns: new[] { "account", "CDATE", "UDATE" },
                values: new object[] { "hebiangu", new DateTime(2023, 11, 14, 17, 43, 38, 515, DateTimeKind.Local).AddTicks(312), new DateTime(2023, 11, 14, 17, 43, 38, 515, DateTimeKind.Local).AddTicks(312) });

            migrationBuilder.UpdateData(
                table: "hi_dd_users",
                keyColumn: "id",
                keyValue: "{E12E19D6-FDD9-4DCE-B211-55E58FAFC207}",
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2023, 11, 14, 17, 43, 38, 515, DateTimeKind.Local).AddTicks(305), new DateTime(2023, 11, 14, 17, 43, 38, 515, DateTimeKind.Local).AddTicks(305) });
        }
    }
}
