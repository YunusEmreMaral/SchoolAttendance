using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAttendance_DataAccessLayer.Migrations
{
    public partial class mig_number_adds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TeacherNumber",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeacherNumber",
                table: "Teachers");
        }
    }
}
