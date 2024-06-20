using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AgainLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacilityCounts_Property_PropertyId",
                table: "FacilityCounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_Descriptions_DescriptionId",
                table: "Property");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_Kinds_KindId",
                table: "Property");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_Locations_LocationId",
                table: "Property");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Property",
                table: "Property");

            migrationBuilder.RenameTable(
                name: "Property",
                newName: "Properties");

            migrationBuilder.RenameIndex(
                name: "IX_Property_LocationId",
                table: "Properties",
                newName: "IX_Properties_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Property_KindId",
                table: "Properties",
                newName: "IX_Properties_KindId");

            migrationBuilder.RenameIndex(
                name: "IX_Property_DescriptionId",
                table: "Properties",
                newName: "IX_Properties_DescriptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Properties",
                table: "Properties",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FacilityCounts_Properties_PropertyId",
                table: "FacilityCounts",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Descriptions_DescriptionId",
                table: "Properties",
                column: "DescriptionId",
                principalTable: "Descriptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Kinds_KindId",
                table: "Properties",
                column: "KindId",
                principalTable: "Kinds",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Locations_LocationId",
                table: "Properties",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacilityCounts_Properties_PropertyId",
                table: "FacilityCounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Descriptions_DescriptionId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Kinds_KindId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Locations_LocationId",
                table: "Properties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Properties",
                table: "Properties");

            migrationBuilder.RenameTable(
                name: "Properties",
                newName: "Property");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_LocationId",
                table: "Property",
                newName: "IX_Property_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_KindId",
                table: "Property",
                newName: "IX_Property_KindId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_DescriptionId",
                table: "Property",
                newName: "IX_Property_DescriptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Property",
                table: "Property",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FacilityCounts_Property_PropertyId",
                table: "FacilityCounts",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Descriptions_DescriptionId",
                table: "Property",
                column: "DescriptionId",
                principalTable: "Descriptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Kinds_KindId",
                table: "Property",
                column: "KindId",
                principalTable: "Kinds",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Locations_LocationId",
                table: "Property",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }
    }
}
