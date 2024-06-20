using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace H.Modules.Identity.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hi_dd_users_hi_dd_roles_role_id",
                table: "hi_dd_users");

            migrationBuilder.AlterColumn<string>(
                name: "role_id",
                table: "hi_dd_users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

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

            migrationBuilder.AddForeignKey(
                name: "FK_hi_dd_users_hi_dd_roles_role_id",
                table: "hi_dd_users",
                column: "role_id",
                principalTable: "hi_dd_roles",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hi_dd_users_hi_dd_roles_role_id",
                table: "hi_dd_users");

            migrationBuilder.AlterColumn<string>(
                name: "role_id",
                table: "hi_dd_users",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_hi_dd_users_hi_dd_roles_role_id",
                table: "hi_dd_users",
                column: "role_id",
                principalTable: "hi_dd_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
