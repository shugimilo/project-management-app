using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementDatabase.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBaseEntityLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualHours",
                table: "WorkPackages");

            migrationBuilder.DropColumn(
                name: "PlannedHours",
                table: "WorkPackages");

            migrationBuilder.DropColumn(
                name: "ActualHours",
                table: "TaskItems");

            migrationBuilder.DropColumn(
                name: "PlannedHours",
                table: "TaskItems");

            migrationBuilder.DropColumn(
                name: "ActualHours",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "PlannedHours",
                table: "Projects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ActualHours",
                table: "WorkPackages",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PlannedHours",
                table: "WorkPackages",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ActualHours",
                table: "TaskItems",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PlannedHours",
                table: "TaskItems",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ActualHours",
                table: "Projects",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PlannedHours",
                table: "Projects",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
