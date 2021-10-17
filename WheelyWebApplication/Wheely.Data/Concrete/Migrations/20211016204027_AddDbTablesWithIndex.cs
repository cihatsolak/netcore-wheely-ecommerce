using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wheely.Data.Concrete.Migrations
{
    public partial class AddDbTablesWithIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StockCode",
                table: "Wheel",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Comment",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 16, 23, 40, 26, 615, DateTimeKind.Local).AddTicks(7765),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 10, 16, 23, 8, 54, 317, DateTimeKind.Local).AddTicks(7738));

            migrationBuilder.CreateIndex(
                name: "IX_Tag_Name",
                table: "Tag",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dimension_Size",
                table: "Dimension",
                column: "Size",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Color_HexCode",
                table: "Color",
                column: "HexCode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tag_Name",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Dimension_Size",
                table: "Dimension");

            migrationBuilder.DropIndex(
                name: "IX_Color_HexCode",
                table: "Color");

            migrationBuilder.AlterColumn<string>(
                name: "StockCode",
                table: "Wheel",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Comment",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 16, 23, 8, 54, 317, DateTimeKind.Local).AddTicks(7738),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 10, 16, 23, 40, 26, 615, DateTimeKind.Local).AddTicks(7765));
        }
    }
}
