using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuestionOverdose.DAL.Migrations
{
    public partial class SetInitialTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "DateOfCreation", "TagName" },
                values: new object[] { 1, new DateTime(2020, 4, 14, 0, 57, 1, 879, DateTimeKind.Local).AddTicks(5215), ".Net" });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "DateOfCreation", "TagName" },
                values: new object[] { 2, new DateTime(2020, 4, 14, 0, 57, 1, 879, DateTimeKind.Local).AddTicks(7296), "Angular" });

            migrationBuilder.InsertData(
                table: "UserTags",
                columns: new[] { "TagId", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "UserTags",
                columns: new[] { "TagId", "UserId" },
                values: new object[] { 2, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserTags",
                keyColumns: new[] { "TagId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "UserTags",
                keyColumns: new[] { "TagId", "UserId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
