using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infra.Migrations
{
    /// <inheritdoc />
    public partial class Temp3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "RecordTemplateConversions",
                newName: "RecordConversions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecordTemplateConversions",
                table: "RecordConversions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecordConversions",
                table: "RecordConversions",
                column: "Id");

            migrationBuilder.DropForeignKey(
                name: "FK_RecordTemplateConversions_RecordTemplateLinks_RecordTemplat~",
                table: "RecordConversions");
            migrationBuilder.AddForeignKey(
                name: "FK_RecordConversions_RecordTemplateLinks_RecordTemplateLinkId",
                table: "RecordConversions",
                column: "RecordTemplateLinkId",
                principalTable: "RecordTemplateLinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.DropForeignKey(
                name: "FK_RecordTemplateConversions_Records_SourceId",
                table: "RecordConversions");
            migrationBuilder.AddForeignKey(
                name: "FK_RecordConversions_Records_SourceId",
                table: "RecordConversions",
                column: "SourceId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.DropForeignKey(
                name: "FK_RecordTemplateConversions_Records_TargetId",
                table: "RecordConversions");
            migrationBuilder.AddForeignKey(
                name: "FK_RecordConversions_Records_TargetId",
                table: "RecordConversions",
                column: "TargetId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.CreateIndex(
                name: "IX_RecordConversions_RecordTemplateLinkId",
                table: "RecordConversions",
                column: "RecordTemplateLinkId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordConversions_SourceId",
                table: "RecordConversions",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordConversions_TargetId",
                table: "RecordConversions",
                column: "TargetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "RecordConversions",
                newName: "RecordTemplateConversions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecordConversions",
                table: "RecordTemplateConversions");
            migrationBuilder.AddPrimaryKey(
                name: "PK_RecordTemplateConversions",
                table: "RecordTemplateConversions",
                column: "Id");

            migrationBuilder.DropForeignKey(
                name: "FK_RecordConversions_RecordTemplateLinks_RecordTemplateLinkId",
                table: "RecordTemplateConversions");
            migrationBuilder.AddForeignKey(
                name: "FK_RecordTemplateConversions_RecordTemplateLinks_RecordTemplat~",
                table: "RecordTemplateConversions",
                column: "RecordTemplateLinkId",
                principalTable: "RecordTemplateLinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.DropForeignKey(
                name: "FK_RecordConversions_Records_SourceId",
                table: "RecordTemplateConversions");
            migrationBuilder.AddForeignKey(
                name: "FK_RecordTemplateConversions_Records_SourceId",
                table: "RecordTemplateConversions",
                column: "SourceId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.DropForeignKey(
                name: "FK_RecordConversions_Records_TargetId",
                table: "RecordTemplateConversions");
            migrationBuilder.AddForeignKey(
                name: "FK_RecordTemplateConversions_Records_TargetId",
                table: "RecordTemplateConversions",
                column: "TargetId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);


            migrationBuilder.CreateIndex(
                name: "IX_RecordTemplateConversions_RecordTemplateLinkId",
                table: "RecordTemplateConversions",
                column: "RecordTemplateLinkId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordTemplateConversions_SourceId",
                table: "RecordTemplateConversions",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordTemplateConversions_TargetId",
                table: "RecordTemplateConversions",
                column: "TargetId");
        }
    }
}
