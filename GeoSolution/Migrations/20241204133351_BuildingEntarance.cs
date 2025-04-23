using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GeoSolution.Migrations
{
    /// <inheritdoc />
    public partial class BuildingEntarance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomBuildings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Geometry = table.Column<string>(type: "text", nullable: false),
                    BuildingType = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CurrentStatus = table.Column<string>(type: "text", nullable: true),
                    OpeningHours = table.Column<string>(type: "text", nullable: true),
                    Color = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomBuildings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntranceDataModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    EntranceType = table.Column<string>(type: "text", nullable: false),
                    AccessLevel = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    CustomBuildingModelId = table.Column<int>(type: "integer", nullable: false),
                    Geometry = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntranceDataModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntranceDataModel_CustomBuildings_CustomBuildingModelId",
                        column: x => x.CustomBuildingModelId,
                        principalTable: "CustomBuildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntranceDataModel_CustomBuildingModelId",
                table: "EntranceDataModel",
                column: "CustomBuildingModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntranceDataModel");

            migrationBuilder.DropTable(
                name: "CustomBuildings");
        }
    }
}
