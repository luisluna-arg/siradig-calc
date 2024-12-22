using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infra.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DataContainterSections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormFields_FormTemplates_FormTemplateId",
                table: "FormFields");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptFields_ReceiptTemplates_ReceiptTemplateId",
                table: "ReceiptFields");

            migrationBuilder.RenameColumn(
                name: "ReceiptTemplateId",
                table: "ReceiptFields",
                newName: "ReceiptTemplateSectionId");

            migrationBuilder.RenameIndex(
                name: "IX_ReceiptFields_ReceiptTemplateId",
                table: "ReceiptFields",
                newName: "IX_ReceiptFields_ReceiptTemplateSectionId");

            migrationBuilder.RenameColumn(
                name: "FormTemplateId",
                table: "FormFields",
                newName: "FormTemplateSectionId");

            migrationBuilder.RenameIndex(
                name: "IX_FormFields_FormTemplateId",
                table: "FormFields",
                newName: "IX_FormFields_FormTemplateSectionId");

            migrationBuilder.CreateTable(
                name: "FormTemplateSection",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FormTemplateId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormTemplateSection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormTemplateSection_FormTemplates_FormTemplateId",
                        column: x => x.FormTemplateId,
                        principalTable: "FormTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReceiptTemplateSection",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReceiptTemplateId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptTemplateSection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptTemplateSection_ReceiptTemplates_ReceiptTemplateId",
                        column: x => x.ReceiptTemplateId,
                        principalTable: "ReceiptTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormTemplateSection_FormTemplateId",
                table: "FormTemplateSection",
                column: "FormTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTemplateSection_ReceiptTemplateId",
                table: "ReceiptTemplateSection",
                column: "ReceiptTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormFields_FormTemplateSection_FormTemplateSectionId",
                table: "FormFields",
                column: "FormTemplateSectionId",
                principalTable: "FormTemplateSection",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptFields_ReceiptTemplateSection_ReceiptTemplateSection~",
                table: "ReceiptFields",
                column: "ReceiptTemplateSectionId",
                principalTable: "ReceiptTemplateSection",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormFields_FormTemplateSection_FormTemplateSectionId",
                table: "FormFields");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptFields_ReceiptTemplateSection_ReceiptTemplateSection~",
                table: "ReceiptFields");

            migrationBuilder.DropTable(
                name: "FormTemplateSection");

            migrationBuilder.DropTable(
                name: "ReceiptTemplateSection");

            migrationBuilder.RenameColumn(
                name: "ReceiptTemplateSectionId",
                table: "ReceiptFields",
                newName: "ReceiptTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_ReceiptFields_ReceiptTemplateSectionId",
                table: "ReceiptFields",
                newName: "IX_ReceiptFields_ReceiptTemplateId");

            migrationBuilder.RenameColumn(
                name: "FormTemplateSectionId",
                table: "FormFields",
                newName: "FormTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_FormFields_FormTemplateSectionId",
                table: "FormFields",
                newName: "IX_FormFields_FormTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormFields_FormTemplates_FormTemplateId",
                table: "FormFields",
                column: "FormTemplateId",
                principalTable: "FormTemplates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptFields_ReceiptTemplates_ReceiptTemplateId",
                table: "ReceiptFields",
                column: "ReceiptTemplateId",
                principalTable: "ReceiptTemplates",
                principalColumn: "Id");
        }
    }
}
