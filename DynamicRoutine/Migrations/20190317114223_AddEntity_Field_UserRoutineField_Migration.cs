using Microsoft.EntityFrameworkCore.Migrations;

namespace DynamicRoutine.Migrations
{
    public partial class AddEntity_Field_UserRoutineField_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "UserRoutineFields",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "UserRoutineFields",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "UserRoutineFields");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "UserRoutineFields");
        }
    }
}
