using Microsoft.EntityFrameworkCore.Migrations;

namespace RollCall.MVC.Migrations
{
    public partial class remAttPercentage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttendancePercentage",
                table: "Attendance");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AttendancePercentage",
                table: "Attendance",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
