using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class RolePolicy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Policies",
                newName: "Policies",
                newSchema: "Membership");

            migrationBuilder.CreateTable(
                name: "RolePolicies",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PolicyId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePolicies", x => new { x.RoleId, x.PolicyId });
                    table.ForeignKey(
                        name: "FK_RolePolicies_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalSchema: "Membership",
                        principalTable: "Policies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RolePolicies_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Membership",
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePolicies_PolicyId",
                table: "RolePolicies",
                column: "PolicyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePolicies");

            migrationBuilder.RenameTable(
                name: "Policies",
                schema: "Membership",
                newName: "Policies");
        }
    }
}
