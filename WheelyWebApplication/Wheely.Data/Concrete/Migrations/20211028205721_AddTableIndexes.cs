using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wheely.Data.Concrete.Migrations
{
    public partial class AddTableIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Wheel",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Comment",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 10, 17, 23, 16, 50, 114, DateTimeKind.Local).AddTicks(6632));

            migrationBuilder.CreateIndex(
                name: "IX_Wheel_Name",
                table: "Wheel",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Wheel_Price",
                table: "Wheel",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_Producer_Name",
                table: "Producer",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Category_Name",
                table: "Category",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Wheel_Name",
                table: "Wheel");

            migrationBuilder.DropIndex(
                name: "IX_Wheel_Price",
                table: "Wheel");

            migrationBuilder.DropIndex(
                name: "IX_Producer_Name",
                table: "Producer");

            migrationBuilder.DropIndex(
                name: "IX_Category_Name",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Wheel");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Comment",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 17, 23, 16, 50, 114, DateTimeKind.Local).AddTicks(6632),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "getdate()");
        }
    }
}
