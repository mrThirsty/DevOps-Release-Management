using Microsoft.EntityFrameworkCore.Migrations;

namespace ReleaseManagement.Framework.Migrations
{
    public partial class UpdatedApprovals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApprovedById",
                table: "ComponentApproval",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "ComponentApproval");
        }
    }
}
