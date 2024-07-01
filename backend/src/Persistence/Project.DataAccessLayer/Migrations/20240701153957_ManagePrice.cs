using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class ManagePrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Property_Prices",
                table: "Properties",
                sql: "[LongPrice] >= [MinPrice] AND [LongPrice] >= [MedPrice] AND [MedPrice] >= [MinPrice]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Property_Prices",
                table: "Properties");
        }
    }
}
