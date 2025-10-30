using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementDatabase.Migrations
{
    /// <inheritdoc />
    public partial class RemovePlannedHoursFromTaskAssignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlannedHours",
                table: "TaskAssignments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PlannedHours",
                table: "TaskAssignments",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
