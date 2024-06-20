using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace H.Modules.Identity.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "role_id",
                table: "hi_dd_users",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.UpdateData(
                table: "hi_dd_authors",
                keyColumn: "id",
                keyValue: "{63860D6B-BD59-4620-919D-0DE51877676F}",
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 2, 19, 29, 8, 829, DateTimeKind.Local).AddTicks(9919), new DateTime(2024, 2, 2, 19, 29, 8, 829, DateTimeKind.Local).AddTicks(9919) });

            migrationBuilder.UpdateData(
                table: "hi_dd_authors",
                keyColumn: "id",
                keyValue: "{DE3B4992-A5BF-4AD2-80D8-2C9654C47A34}",
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 2, 19, 29, 8, 829, DateTimeKind.Local).AddTicks(9945), new DateTime(2024, 2, 2, 19, 29, 8, 829, DateTimeKind.Local).AddTicks(9945) });

            migrationBuilder.UpdateData(
                table: "hi_dd_roles",
                keyColumn: "id",
                keyValue: "{0E465AF1-4C5B-417B-B496-E74E8A0D7E5C}",
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 2, 19, 29, 8, 829, DateTimeKind.Local).AddTicks(9966), new DateTime(2024, 2, 2, 19, 29, 8, 829, DateTimeKind.Local).AddTicks(9966) });

            migrationBuilder.UpdateData(
                table: "hi_dd_roles",
                keyColumn: "id",
                keyValue: "{4360CE12-E5F4-4EA6-937C-9FDEA4DF06F6}",
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 2, 19, 29, 8, 829, DateTimeKind.Local).AddTicks(9958), new DateTime(2024, 2, 2, 19, 29, 8, 829, DateTimeKind.Local).AddTicks(9958) });

            migrationBuilder.UpdateData(
                table: "hi_dd_users",
                keyColumn: "id",
                keyValue: "{A8EE1331-0DA7-42F1-80E2-CD2A20D62BC9}",
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 2, 19, 29, 8, 829, DateTimeKind.Local).AddTicks(9980), new DateTime(2024, 2, 2, 19, 29, 8, 829, DateTimeKind.Local).AddTicks(9980) });

            migrationBuilder.UpdateData(
                table: "hi_dd_users",
                keyColumn: "id",
                keyValue: "{E12E19D6-FDD9-4DCE-B211-55E58FAFC207}",
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 2, 19, 29, 8, 829, DateTimeKind.Local).AddTicks(9972), new DateTime(2024, 2, 2, 19, 29, 8, 829, DateTimeKind.Local).AddTicks(9972) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "role_id",
                table: "hi_dd_users",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 6);

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
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 2, 19, 18, 56, 642, DateTimeKind.Local).AddTicks(9503), new DateTime(2024, 2, 2, 19, 18, 56, 642, DateTimeKind.Local).AddTicks(9503) });

            migrationBuilder.UpdateData(
                table: "hi_dd_users",
                keyColumn: "id",
                keyValue: "{E12E19D6-FDD9-4DCE-B211-55E58FAFC207}",
                columns: new[] { "CDATE", "UDATE" },
                values: new object[] { new DateTime(2024, 2, 2, 19, 18, 56, 642, DateTimeKind.Local).AddTicks(9480), new DateTime(2024, 2, 2, 19, 18, 56, 642, DateTimeKind.Local).AddTicks(9480) });
        }
    }
}
