using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoSolution.Migrations
{
    /// <inheritdoc />
    public partial class Entarance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntranceDataModel_CustomBuildings_CustomBuildingModelId",
                table: "EntranceDataModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EntranceDataModel",
                table: "EntranceDataModel");

            migrationBuilder.RenameTable(
                name: "EntranceDataModel",
                newName: "EntranceDataModels");

            migrationBuilder.RenameIndex(
                name: "IX_EntranceDataModel_CustomBuildingModelId",
                table: "EntranceDataModels",
                newName: "IX_EntranceDataModels_CustomBuildingModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EntranceDataModels",
                table: "EntranceDataModels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EntranceDataModels_CustomBuildings_CustomBuildingModelId",
                table: "EntranceDataModels",
                column: "CustomBuildingModelId",
                principalTable: "CustomBuildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntranceDataModels_CustomBuildings_CustomBuildingModelId",
                table: "EntranceDataModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EntranceDataModels",
                table: "EntranceDataModels");

            migrationBuilder.RenameTable(
                name: "EntranceDataModels",
                newName: "EntranceDataModel");

            migrationBuilder.RenameIndex(
                name: "IX_EntranceDataModels_CustomBuildingModelId",
                table: "EntranceDataModel",
                newName: "IX_EntranceDataModel_CustomBuildingModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EntranceDataModel",
                table: "EntranceDataModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EntranceDataModel_CustomBuildings_CustomBuildingModelId",
                table: "EntranceDataModel",
                column: "CustomBuildingModelId",
                principalTable: "CustomBuildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
