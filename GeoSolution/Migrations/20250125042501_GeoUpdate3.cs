using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace GeoSolution.Migrations
{
    /// <inheritdoc />
    public partial class GeoUpdate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<MultiPolygon>(
                name: "Geometry",
                table: "CustomBuildings",
                type: "geometry(MultiPolygon, 4326)",
                nullable: false,
                oldClrType: typeof(MultiPolygon),
                oldType: "geometry");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<MultiPolygon>(
                name: "Geometry",
                table: "CustomBuildings",
                type: "geometry",
                nullable: false,
                oldClrType: typeof(MultiPolygon),
                oldType: "geometry(MultiPolygon, 4326)");
        }
    }
}
