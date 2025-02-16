using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infra.Migrations
{
    /// <inheritdoc />
    public partial class Temp2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecordFieldLinks_RecordTemplateFields_RecordTemplateFieldId",
                table: "RecordFieldLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_RecordTemplates_TemplateId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_RecordTemplateConversions_RecordTemplateLinks_RecordTemplat~",
                table: "RecordTemplateConversions");

            migrationBuilder.DropForeignKey(
                name: "FK_RecordTemplateFields_RecordTemplateSections_RecordTemplateS~",
                table: "RecordTemplateFields");

            migrationBuilder.DropForeignKey(
                name: "FK_RecordTemplateSections_RecordTemplates_RecordTemplateId",
                table: "RecordTemplateSections");

            migrationBuilder.AddColumn<Guid>(
                name: "RecordTemplateId",
                table: "RecordTemplateLinks",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecordTemplateLinks_RecordTemplateId",
                table: "RecordTemplateLinks",
                column: "RecordTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecordFieldLinks_RecordTemplateFields_RecordTemplateFieldId",
                table: "RecordFieldLinks",
                column: "RecordTemplateFieldId",
                principalTable: "RecordTemplateFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_RecordTemplates_TemplateId",
                table: "Records",
                column: "TemplateId",
                principalTable: "RecordTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecordTemplateConversions_RecordTemplateLinks_RecordTemplat~",
                table: "RecordTemplateConversions",
                column: "RecordTemplateLinkId",
                principalTable: "RecordTemplateLinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecordTemplateFields_RecordTemplateSections_RecordTemplateS~",
                table: "RecordTemplateFields",
                column: "RecordTemplateSectionId",
                principalTable: "RecordTemplateSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecordTemplateLinks_RecordTemplates_RecordTemplateId",
                table: "RecordTemplateLinks",
                column: "RecordTemplateId",
                principalTable: "RecordTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecordTemplateSections_RecordTemplates_RecordTemplateId",
                table: "RecordTemplateSections",
                column: "RecordTemplateId",
                principalTable: "RecordTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecordFieldLinks_RecordTemplateFields_RecordTemplateFieldId",
                table: "RecordFieldLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_RecordTemplates_TemplateId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_RecordTemplateConversions_RecordTemplateLinks_RecordTemplat~",
                table: "RecordTemplateConversions");

            migrationBuilder.DropForeignKey(
                name: "FK_RecordTemplateFields_RecordTemplateSections_RecordTemplateS~",
                table: "RecordTemplateFields");

            migrationBuilder.DropForeignKey(
                name: "FK_RecordTemplateLinks_RecordTemplates_RecordTemplateId",
                table: "RecordTemplateLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_RecordTemplateSections_RecordTemplates_RecordTemplateId",
                table: "RecordTemplateSections");

            migrationBuilder.DropIndex(
                name: "IX_RecordTemplateLinks_RecordTemplateId",
                table: "RecordTemplateLinks");

            migrationBuilder.DropColumn(
                name: "RecordTemplateId",
                table: "RecordTemplateLinks");

            migrationBuilder.AddForeignKey(
                name: "FK_RecordFieldLinks_RecordTemplateFields_RecordTemplateFieldId",
                table: "RecordFieldLinks",
                column: "RecordTemplateFieldId",
                principalTable: "RecordTemplateFields",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_RecordTemplates_TemplateId",
                table: "Records",
                column: "TemplateId",
                principalTable: "RecordTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecordTemplateConversions_RecordTemplateLinks_RecordTemplat~",
                table: "RecordTemplateConversions",
                column: "RecordTemplateLinkId",
                principalTable: "RecordTemplateLinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecordTemplateFields_RecordTemplateSections_RecordTemplateS~",
                table: "RecordTemplateFields",
                column: "RecordTemplateSectionId",
                principalTable: "RecordTemplateSections",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecordTemplateSections_RecordTemplates_RecordTemplateId",
                table: "RecordTemplateSections",
                column: "RecordTemplateId",
                principalTable: "RecordTemplates",
                principalColumn: "Id");
        }
    }
}
