using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class PassswordReset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordResetToken",
                schema: "Membership",
                table: "Users",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordResetTokenGeneratedAt",
                schema: "Membership",
                table: "Users",
                type: "datetime",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordResetToken",
                schema: "Membership",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordResetTokenGeneratedAt",
                schema: "Membership",
                table: "Users");
        }
    }
}
