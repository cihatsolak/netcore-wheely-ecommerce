using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wheely.Data.Concrete.Migrations
{
    public partial class ChangePictureEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Picture",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Comment",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 17, 23, 16, 50, 114, DateTimeKind.Local).AddTicks(6632),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 10, 16, 23, 40, 26, 615, DateTimeKind.Local).AddTicks(7765));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Picture");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Comment",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 16, 23, 40, 26, 615, DateTimeKind.Local).AddTicks(7765),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 10, 17, 23, 16, 50, 114, DateTimeKind.Local).AddTicks(6632));
        }
    }
}
