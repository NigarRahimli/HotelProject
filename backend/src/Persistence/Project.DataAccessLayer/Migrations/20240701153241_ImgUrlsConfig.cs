using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class ImgUrlsConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_User_UserId",
                table: "Likes");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FacilityCounts",
                table: "FacilityCounts");

            migrationBuilder.DropIndex(
                name: "IX_FacilityCounts_PropertyId",
                table: "FacilityCounts");

            migrationBuilder.AddColumn<string>(
                name: "ProfileImgUrl",
                schema: "Membership",
                table: "Users",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IconUrl",
                table: "Safeties",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IconUrl",
                table: "Amenities",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FacilityCounts",
                table: "FacilityCounts",
                columns: new[] { "PropertyId", "FacilityId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Users_UserId",
                table: "Likes",
                column: "UserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Users_UserId",
                table: "Likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FacilityCounts",
                table: "FacilityCounts");

            migrationBuilder.DropColumn(
                name: "ProfileImgUrl",
                schema: "Membership",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IconUrl",
                table: "Safeties");

            migrationBuilder.DropColumn(
                name: "IconUrl",
                table: "Amenities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FacilityCounts",
                table: "FacilityCounts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacilityCounts_PropertyId",
                table: "FacilityCounts",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_User_UserId",
                table: "Likes",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
