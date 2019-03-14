using Microsoft.EntityFrameworkCore.Migrations;

namespace DynamicRoutine.Migrations
{
    public partial class UpdateDataType_ToStep_mMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ToStep",
                table: "RoutineSteps",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ToStep",
                table: "RoutineLog",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ToStep",
                table: "RoutineSteps",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ToStep",
                table: "RoutineLog",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
