using Microsoft.EntityFrameworkCore.Migrations;

namespace DynamicRoutine.Migrations
{
    public partial class delete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoutineStep_Routines_RoutineId",
                table: "RoutineStep");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoutineStep",
                table: "RoutineStep");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "RoutineStep");

            migrationBuilder.RenameTable(
                name: "RoutineStep",
                newName: "RoutineSteps");

            migrationBuilder.RenameIndex(
                name: "IX_RoutineStep_RoutineId",
                table: "RoutineSteps",
                newName: "IX_RoutineSteps_RoutineId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoutineSteps",
                table: "RoutineSteps",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoutineSteps_Routines_RoutineId",
                table: "RoutineSteps",
                column: "RoutineId",
                principalTable: "Routines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoutineSteps_Routines_RoutineId",
                table: "RoutineSteps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoutineSteps",
                table: "RoutineSteps");

            migrationBuilder.RenameTable(
                name: "RoutineSteps",
                newName: "RoutineStep");

            migrationBuilder.RenameIndex(
                name: "IX_RoutineSteps_RoutineId",
                table: "RoutineStep",
                newName: "IX_RoutineStep_RoutineId");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "RoutineStep",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoutineStep",
                table: "RoutineStep",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoutineStep_Routines_RoutineId",
                table: "RoutineStep",
                column: "RoutineId",
                principalTable: "Routines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
