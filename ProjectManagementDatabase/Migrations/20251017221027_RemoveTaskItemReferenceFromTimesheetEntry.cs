using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementDatabase.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTaskItemReferenceFromTimesheetEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimesheetEntries_TaskItems_TaskItemId",
                table: "TimesheetEntries");

            migrationBuilder.AlterColumn<int>(
                name: "TaskItemId",
                table: "TimesheetEntries",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_TimesheetEntries_TaskItems_TaskItemId",
                table: "TimesheetEntries",
                column: "TaskItemId",
                principalTable: "TaskItems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimesheetEntries_TaskItems_TaskItemId",
                table: "TimesheetEntries");

            migrationBuilder.AlterColumn<int>(
                name: "TaskItemId",
                table: "TimesheetEntries",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TimesheetEntries_TaskItems_TaskItemId",
                table: "TimesheetEntries",
                column: "TaskItemId",
                principalTable: "TaskItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
