using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameWorkCoreH3.Migrations
{
    /// <inheritdoc />
    public partial class FixTeamRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentTodoId",
                table: "Workers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentTaskId",
                table: "Teams",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workers_CurrentTodoId",
                table: "Workers",
                column: "CurrentTodoId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CurrentTaskId",
                table: "Teams",
                column: "CurrentTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Tasks_CurrentTaskId",
                table: "Teams",
                column: "CurrentTaskId",
                principalTable: "Tasks",
                principalColumn: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Todo_CurrentTodoId",
                table: "Workers",
                column: "CurrentTodoId",
                principalTable: "Todo",
                principalColumn: "TodoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Tasks_CurrentTaskId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Todo_CurrentTodoId",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Workers_CurrentTodoId",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Teams_CurrentTaskId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "CurrentTodoId",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "CurrentTaskId",
                table: "Teams");
        }
    }
}
