using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infra.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRecordConversions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReceiptToFormConversions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RecordTemplateLinkId = table.Column<Guid>(type: "uuid", nullable: false),
                    SourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptToFormConversions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptToFormConversions_Forms_TargetId",
                        column: x => x.TargetId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiptToFormConversions_Receipts_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Receipts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiptToFormConversions_RecordTemplateLinks_RecordTemplate~",
                        column: x => x.RecordTemplateLinkId,
                        principalTable: "RecordTemplateLinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptToFormConversions_RecordTemplateLinkId",
                table: "ReceiptToFormConversions",
                column: "RecordTemplateLinkId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptToFormConversions_SourceId",
                table: "ReceiptToFormConversions",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptToFormConversions_TargetId",
                table: "ReceiptToFormConversions",
                column: "TargetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceiptToFormConversions");
        }
    }
}
