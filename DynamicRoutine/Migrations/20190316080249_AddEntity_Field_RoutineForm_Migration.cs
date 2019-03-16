using Microsoft.EntityFrameworkCore.Migrations;

namespace DynamicRoutine.Migrations
{
    public partial class AddEntity_Field_RoutineForm_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FromStep",
                table: "RoutineForms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "PreviousIsEdit",
                table: "RoutineForms",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromStep",
                table: "RoutineForms");

            migrationBuilder.DropColumn(
                name: "PreviousIsEdit",
                table: "RoutineForms");
        }
    }
}
