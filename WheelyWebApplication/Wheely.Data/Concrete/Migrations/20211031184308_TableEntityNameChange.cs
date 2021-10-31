using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Wheely.Data.Concrete.Migrations
{
    public partial class TableEntityNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    HexCode = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dimension",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Size = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dimension", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Producer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Route",
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
                    table.PrimaryKey("PK_Route", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Route_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wheel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StarCount = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    ShortDescription = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    StockCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CampaignPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    ProducerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wheel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wheel_Producer_ProducerId",
                        column: x => x.ProducerId,
                        principalTable: "Producer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryWheel",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "integer", nullable: false),
                    WheelsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryWheel", x => new { x.CategoriesId, x.WheelsId });
                    table.ForeignKey(
                        name: "FK_CategoryWheel_Category_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryWheel_Wheel_WheelsId",
                        column: x => x.WheelsId,
                        principalTable: "Wheel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ColorWheel",
                columns: table => new
                {
                    ColorsId = table.Column<int>(type: "integer", nullable: false),
                    WheelsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorWheel", x => new { x.ColorsId, x.WheelsId });
                    table.ForeignKey(
                        name: "FK_ColorWheel_Color_ColorsId",
                        column: x => x.ColorsId,
                        principalTable: "Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColorWheel_Wheel_WheelsId",
                        column: x => x.WheelsId,
                        principalTable: "Wheel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    StarCount = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    Path = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    WheelId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Wheel_WheelId",
                        column: x => x.WheelId,
                        principalTable: "Wheel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DimensionWheel",
                columns: table => new
                {
                    DimensionsId = table.Column<int>(type: "integer", nullable: false),
                    WheelsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimensionWheel", x => new { x.DimensionsId, x.WheelsId });
                    table.ForeignKey(
                        name: "FK_DimensionWheel_Dimension_DimensionsId",
                        column: x => x.DimensionsId,
                        principalTable: "Dimension",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DimensionWheel_Wheel_WheelsId",
                        column: x => x.WheelsId,
                        principalTable: "Wheel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Picture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Path = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    WheelId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Picture_Wheel_WheelId",
                        column: x => x.WheelId,
                        principalTable: "Wheel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagWheel",
                columns: table => new
                {
                    TagsId = table.Column<int>(type: "integer", nullable: false),
                    WheelsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagWheel", x => new { x.TagsId, x.WheelsId });
                    table.ForeignKey(
                        name: "FK_TagWheel_Tag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagWheel_Wheel_WheelsId",
                        column: x => x.WheelsId,
                        principalTable: "Wheel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_Name",
                table: "Category",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryWheel_WheelsId",
                table: "CategoryWheel",
                column: "WheelsId");

            migrationBuilder.CreateIndex(
                name: "IX_Color_HexCode",
                table: "Color",
                column: "HexCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ColorWheel_WheelsId",
                table: "ColorWheel",
                column: "WheelsId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_WheelId",
                table: "Comment",
                column: "WheelId");

            migrationBuilder.CreateIndex(
                name: "IX_Dimension_Size",
                table: "Dimension",
                column: "Size",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DimensionWheel_WheelsId",
                table: "DimensionWheel",
                column: "WheelsId");

            migrationBuilder.CreateIndex(
                name: "IX_Module_Name",
                table: "Module",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Picture_WheelId",
                table: "Picture",
                column: "WheelId");

            migrationBuilder.CreateIndex(
                name: "IX_Producer_Name",
                table: "Producer",
                column: "Name");

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

            migrationBuilder.CreateIndex(
                name: "IX_Tag_Name",
                table: "Tag",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TagWheel_WheelsId",
                table: "TagWheel",
                column: "WheelsId");

            migrationBuilder.CreateIndex(
                name: "IX_Wheel_Name",
                table: "Wheel",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Wheel_Price",
                table: "Wheel",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_Wheel_ProducerId",
                table: "Wheel",
                column: "ProducerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryWheel");

            migrationBuilder.DropTable(
                name: "ColorWheel");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "DimensionWheel");

            migrationBuilder.DropTable(
                name: "Picture");

            migrationBuilder.DropTable(
                name: "Route");

            migrationBuilder.DropTable(
                name: "TagWheel");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "Dimension");

            migrationBuilder.DropTable(
                name: "Module");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Wheel");

            migrationBuilder.DropTable(
                name: "Producer");
        }
    }
}
