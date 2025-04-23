using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace GeoSolution.Migrations
{
    /// <inheritdoc />
    public partial class GeoUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.AlterColumn<Geometry>(
                name: "Geometry",
                table: "CustomBuildings",
                type: "geometry",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.AlterColumn<string>(
                name: "Geometry",
                table: "CustomBuildings",
                type: "text",
                nullable: false,
                oldClrType: typeof(Geometry),
                oldType: "geometry");
        }
    }
}
