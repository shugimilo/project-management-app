using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementDatabase.Migrations
{
    /// <inheritdoc />
    public partial class AddTeamToWorkPackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "WorkPackages",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkPackages_TeamId",
                table: "WorkPackages",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkPackages_Teams_TeamId",
                table: "WorkPackages",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkPackages_Teams_TeamId",
                table: "WorkPackages");

            migrationBuilder.DropIndex(
                name: "IX_WorkPackages_TeamId",
                table: "WorkPackages");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "WorkPackages");
        }
    }
}
