using Microsoft.EntityFrameworkCore.Migrations;

namespace DynamicRoutine.Migrations
{
    public partial class AddField_TitleEn_RoutineField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TitleEn",
                table: "RoutineFields",
                maxLength: 300,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TitleEn",
                table: "RoutineFields");
        }
    }
}
