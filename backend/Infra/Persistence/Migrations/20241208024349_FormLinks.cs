using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infra.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FormLinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataContainerFieldLinks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FormFieldId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReceiptFieldId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataContainerFieldLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataContainerFieldLinks_FormFields_FormFieldId",
                        column: x => x.FormFieldId,
                        principalTable: "FormFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataContainerFieldLinks_ReceiptFields_ReceiptFieldId",
                        column: x => x.ReceiptFieldId,
                        principalTable: "ReceiptFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DataContainerLinks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FormTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReceiptTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataContainerLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataContainerLinks_FormTemplates_FormTemplateId",
                        column: x => x.FormTemplateId,
                        principalTable: "FormTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataContainerLinks_ReceiptTemplates_ReceiptTemplateId",
                        column: x => x.ReceiptTemplateId,
                        principalTable: "ReceiptTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataContainerFieldLinks_FormFieldId",
                table: "DataContainerFieldLinks",
                column: "FormFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_DataContainerFieldLinks_ReceiptFieldId",
                table: "DataContainerFieldLinks",
                column: "ReceiptFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_DataContainerLinks_FormTemplateId",
                table: "DataContainerLinks",
                column: "FormTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_DataContainerLinks_ReceiptTemplateId",
                table: "DataContainerLinks",
                column: "ReceiptTemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataContainerFieldLinks");

            migrationBuilder.DropTable(
                name: "DataContainerLinks");
        }
    }
}
