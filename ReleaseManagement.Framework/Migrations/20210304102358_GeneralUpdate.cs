using Microsoft.EntityFrameworkCore.Migrations;

namespace ReleaseManagement.Framework.Migrations
{
    public partial class GeneralUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentApproval_Component_ComponentId",
                table: "ComponentApproval");

            migrationBuilder.AddColumn<int>(
                name: "ReleaseId1",
                table: "ComponentApproval",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComponentApproval_ReleaseId1",
                table: "ComponentApproval",
                column: "ReleaseId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentApproval_Component_ComponentId",
                table: "ComponentApproval",
                column: "ComponentId",
                principalTable: "Component",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentApproval_Release_ReleaseId1",
                table: "ComponentApproval",
                column: "ReleaseId1",
                principalTable: "Release",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentApproval_Component_ComponentId",
                table: "ComponentApproval");

            migrationBuilder.DropForeignKey(
                name: "FK_ComponentApproval_Release_ReleaseId1",
                table: "ComponentApproval");

            migrationBuilder.DropIndex(
                name: "IX_ComponentApproval_ReleaseId1",
                table: "ComponentApproval");

            migrationBuilder.DropColumn(
                name: "ReleaseId1",
                table: "ComponentApproval");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentApproval_Component_ComponentId",
                table: "ComponentApproval",
                column: "ComponentId",
                principalTable: "Component",
                principalColumn: "Id");
        }
    }
}
