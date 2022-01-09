using Microsoft.EntityFrameworkCore.Migrations;

namespace RPL.Infrastructure.Migrations
{
    public partial class BookingDoctorSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DoctorScheduleId",
                table: "Bookings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_DoctorScheduleId",
                table: "Bookings",
                column: "DoctorScheduleId");

          
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookings_DoctorScheduleId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "DoctorScheduleId",
                table: "Bookings");
        }
    }
}
