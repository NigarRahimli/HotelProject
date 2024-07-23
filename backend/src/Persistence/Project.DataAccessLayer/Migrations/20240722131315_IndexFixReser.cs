using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class IndexFixReser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reservations_PropertyId_CheckInTime_CheckOutTime",
                table: "Reservations");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_PropertyId_CheckInTime_CheckOutTime",
                table: "Reservations",
                columns: new[] { "PropertyId", "CheckInTime", "CheckOutTime" },
                unique: true,
                filter: "[ReservationStatus] = 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reservations_PropertyId_CheckInTime_CheckOutTime",
                table: "Reservations");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_PropertyId_CheckInTime_CheckOutTime",
                table: "Reservations",
                columns: new[] { "PropertyId", "CheckInTime", "CheckOutTime" },
                unique: true,
                filter: "[ReservationStatus] = 0");
        }
    }
}
