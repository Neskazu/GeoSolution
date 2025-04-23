using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoSolution.Migrations
{
    /// <inheritdoc />
    public partial class Entarance3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntranceDataModels_CustomBuildings_CustomBuildingModelId",
                table: "EntranceDataModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EntranceDataModels",
                table: "EntranceDataModels");

            migrationBuilder.RenameTable(
                name: "EntranceDataModels",
                newName: "EntranceDatas");

            migrationBuilder.RenameIndex(
                name: "IX_EntranceDataModels_CustomBuildingModelId",
                table: "EntranceDatas",
                newName: "IX_EntranceDatas_CustomBuildingModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EntranceDatas",
                table: "EntranceDatas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EntranceDatas_CustomBuildings_CustomBuildingModelId",
                table: "EntranceDatas",
                column: "CustomBuildingModelId",
                principalTable: "CustomBuildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntranceDatas_CustomBuildings_CustomBuildingModelId",
                table: "EntranceDatas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EntranceDatas",
                table: "EntranceDatas");

            migrationBuilder.RenameTable(
                name: "EntranceDatas",
                newName: "EntranceDataModels");

            migrationBuilder.RenameIndex(
                name: "IX_EntranceDatas_CustomBuildingModelId",
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
    }
}
