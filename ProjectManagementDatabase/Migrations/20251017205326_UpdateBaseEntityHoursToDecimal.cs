using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementDatabase.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBaseEntityHoursToDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PlannedHours",
                table: "WorkPackages",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ActualHours",
                table: "WorkPackages",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PlannedHours",
                table: "TaskItems",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ActualHours",
                table: "TaskItems",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PlannedHours",
                table: "Projects",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ActualHours",
                table: "Projects",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PlannedHours",
                table: "Activities",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ActualHours",
                table: "Activities",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PlannedHours",
                table: "WorkPackages",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "ActualHours",
                table: "WorkPackages",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "PlannedHours",
                table: "TaskItems",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "ActualHours",
                table: "TaskItems",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "PlannedHours",
                table: "Projects",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "ActualHours",
                table: "Projects",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "PlannedHours",
                table: "Activities",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "ActualHours",
                table: "Activities",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT");
        }
    }
}
