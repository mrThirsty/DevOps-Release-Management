using Microsoft.EntityFrameworkCore.Migrations;

namespace ReleaseManagement.Framework.Migrations
{
    public partial class ComponentUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Component_ComponentType_ComponentTypeId",
                table: "Component");

            migrationBuilder.DropForeignKey(
                name: "FK_ComponentApproval_Component_ComponentId",
                table: "ComponentApproval");

            migrationBuilder.AddColumn<int>(
                name: "ComponentId1",
                table: "ComponentApproval",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ComponentTypeId1",
                table: "Component",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComponentApproval_ComponentId1",
                table: "ComponentApproval",
                column: "ComponentId1");

            migrationBuilder.CreateIndex(
                name: "IX_Component_ComponentTypeId1",
                table: "Component",
                column: "ComponentTypeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Component_ComponentType_ComponentTypeId",
                table: "Component",
                column: "ComponentTypeId",
                principalTable: "ComponentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Component_ComponentType_ComponentTypeId1",
                table: "Component",
                column: "ComponentTypeId1",
                principalTable: "ComponentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentApproval_Component_ComponentId",
                table: "ComponentApproval",
                column: "ComponentId",
                principalTable: "Component",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentApproval_Component_ComponentId1",
                table: "ComponentApproval",
                column: "ComponentId1",
                principalTable: "Component",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Component_ComponentType_ComponentTypeId",
                table: "Component");

            migrationBuilder.DropForeignKey(
                name: "FK_Component_ComponentType_ComponentTypeId1",
                table: "Component");

            migrationBuilder.DropForeignKey(
                name: "FK_ComponentApproval_Component_ComponentId",
                table: "ComponentApproval");

            migrationBuilder.DropForeignKey(
                name: "FK_ComponentApproval_Component_ComponentId1",
                table: "ComponentApproval");

            migrationBuilder.DropIndex(
                name: "IX_ComponentApproval_ComponentId1",
                table: "ComponentApproval");

            migrationBuilder.DropIndex(
                name: "IX_Component_ComponentTypeId1",
                table: "Component");

            migrationBuilder.DropColumn(
                name: "ComponentId1",
                table: "ComponentApproval");

            migrationBuilder.DropColumn(
                name: "ComponentTypeId1",
                table: "Component");

            migrationBuilder.AddForeignKey(
                name: "FK_Component_ComponentType_ComponentTypeId",
                table: "Component",
                column: "ComponentTypeId",
                principalTable: "ComponentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentApproval_Component_ComponentId",
                table: "ComponentApproval",
                column: "ComponentId",
                principalTable: "Component",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
