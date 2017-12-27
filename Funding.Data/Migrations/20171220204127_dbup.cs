using Microsoft.EntityFrameworkCore.Migrations;

namespace Funding.Data.Migrations
{
    public partial class dbup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsTags_Projects_ProjectId",
                table: "ProjectsTags");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsTags_Tags_TagId",
                table: "ProjectsTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectsTags",
                table: "ProjectsTags");

            migrationBuilder.RenameTable(
                name: "ProjectsTags",
                newName: "ProjectTags");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectsTags_TagId",
                table: "ProjectTags",
                newName: "IX_ProjectTags_TagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectTags",
                table: "ProjectTags",
                columns: new[] { "ProjectId", "TagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTags_Projects_ProjectId",
                table: "ProjectTags",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTags_Tags_TagId",
                table: "ProjectTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTags_Projects_ProjectId",
                table: "ProjectTags");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTags_Tags_TagId",
                table: "ProjectTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectTags",
                table: "ProjectTags");

            migrationBuilder.RenameTable(
                name: "ProjectTags",
                newName: "ProjectsTags");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectTags_TagId",
                table: "ProjectsTags",
                newName: "IX_ProjectsTags_TagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectsTags",
                table: "ProjectsTags",
                columns: new[] { "ProjectId", "TagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsTags_Projects_ProjectId",
                table: "ProjectsTags",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsTags_Tags_TagId",
                table: "ProjectsTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}