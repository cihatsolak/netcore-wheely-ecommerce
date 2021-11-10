using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Wheely.Data.Concrete.Migrations
{
    public partial class ChangeEntityTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Route");

            migrationBuilder.CreateTable(
                name: "RouteValueTransform",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ControllerName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ActionName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SlugUrl = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CustomUrl = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    EntityId = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    ModuleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteValueTransform", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RouteValueTransform_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RouteValueTransform_CustomUrl",
                table: "RouteValueTransform",
                column: "CustomUrl",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RouteValueTransform_ModuleId",
                table: "RouteValueTransform",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteValueTransform_SlugUrl",
                table: "RouteValueTransform",
                column: "SlugUrl",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RouteValueTransform");

            migrationBuilder.CreateTable(
                name: "Route",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActionName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ControllerName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    CustomUrl = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    EntityId = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    ModuleId = table.Column<int>(type: "integer", nullable: false),
                    SlugUrl = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Route", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Route_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Route_CustomUrl",
                table: "Route",
                column: "CustomUrl",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Route_ModuleId",
                table: "Route",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Route_SlugUrl",
                table: "Route",
                column: "SlugUrl",
                unique: true);
        }
    }
}
