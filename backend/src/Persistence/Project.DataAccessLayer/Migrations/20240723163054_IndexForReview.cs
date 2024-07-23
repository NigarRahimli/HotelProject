using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class IndexForReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Unique_User_Property_Category",
                table: "Reviews",
                columns: new[] { "CreatedBy", "PropertyId", "CategoryId" },
                unique: true,
                filter: "[CreatedBy] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Unique_User_Property_Category",
                table: "Reviews");
        }
    }
}
