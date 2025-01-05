using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infra.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenamedRecordTemplateReference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Forms_FormTemplates_RecordId",
                table: "Forms");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_ReceiptTemplates_RecordId",
                table: "Receipts");

            migrationBuilder.RenameColumn(
                name: "RecordId",
                table: "Receipts",
                newName: "RecordTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_Receipts_RecordId",
                table: "Receipts",
                newName: "IX_Receipts_RecordTemplateId");

            migrationBuilder.RenameColumn(
                name: "RecordId",
                table: "Forms",
                newName: "RecordTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_Forms_RecordId",
                table: "Forms",
                newName: "IX_Forms_RecordTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_FormTemplates_RecordTemplateId",
                table: "Forms",
                column: "RecordTemplateId",
                principalTable: "FormTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_ReceiptTemplates_RecordTemplateId",
                table: "Receipts",
                column: "RecordTemplateId",
                principalTable: "ReceiptTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Forms_FormTemplates_RecordTemplateId",
                table: "Forms");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_ReceiptTemplates_RecordTemplateId",
                table: "Receipts");

            migrationBuilder.RenameColumn(
                name: "RecordTemplateId",
                table: "Receipts",
                newName: "RecordId");

            migrationBuilder.RenameIndex(
                name: "IX_Receipts_RecordTemplateId",
                table: "Receipts",
                newName: "IX_Receipts_RecordId");

            migrationBuilder.RenameColumn(
                name: "RecordTemplateId",
                table: "Forms",
                newName: "RecordId");

            migrationBuilder.RenameIndex(
                name: "IX_Forms_RecordTemplateId",
                table: "Forms",
                newName: "IX_Forms_RecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_FormTemplates_RecordId",
                table: "Forms",
                column: "RecordId",
                principalTable: "FormTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_ReceiptTemplates_RecordId",
                table: "Receipts",
                column: "RecordId",
                principalTable: "ReceiptTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
