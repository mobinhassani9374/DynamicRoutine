using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DynamicRoutine.Migrations
{
    public partial class AddEntity_RoutineCustomActionField_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoutineCustomActionFields",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FieldId = table.Column<int>(nullable: false),
                    Operation = table.Column<int>(nullable: false),
                    RoutineCustomActionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutineCustomActionFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoutineCustomActionFields_RoutineCustomAction_RoutineCustomActionId",
                        column: x => x.RoutineCustomActionId,
                        principalTable: "RoutineCustomAction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoutineCustomActionFields_RoutineCustomActionId",
                table: "RoutineCustomActionFields",
                column: "RoutineCustomActionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoutineCustomActionFields");
        }
    }
}
