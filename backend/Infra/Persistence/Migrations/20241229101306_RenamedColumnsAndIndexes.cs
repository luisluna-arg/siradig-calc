using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infra.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenamedColumnsAndIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameColumn(
                name: "ReceiptId",
                newName: "RecordId",
                table: "ReceiptValues");

            migrationBuilder.RenameColumn(
                name: "FormId",
                newName: "RecordId",
                table: "FormValues");

            migrationBuilder.DropForeignKey(
                name: "FK_FormValues_Forms_FormId",
                table: "FormValues");

            migrationBuilder.AddForeignKey(
                name: "FK_FormValues_Forms_RecordId",
                table: "FormValues",
                column: "RecordId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptValues_Receipts_ReceiptId",
                table: "ReceiptValues");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptValues_Receipts_RecordId",
                table: "ReceiptValues",
                column: "RecordId",
                principalTable: "Receipts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.RenameIndex(
                name: "IX_ReceiptValues_ReceiptId",
                newName: "IX_ReceiptValues_RecordId",
                table: "ReceiptValues");

            migrationBuilder.RenameIndex(
                name: "IX_FormValues_FormId",
                newName: "IX_FormValues_RecordId",
                table: "FormValues");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameIndex(
                name: "IX_ReceiptValues_RecordId",
                newName: "IX_ReceiptValues_ReceiptId",
                table: "ReceiptValues");

            migrationBuilder.RenameIndex(
                name: "IX_FormValues_RecordId",
                newName: "IX_FormValues_FormId",
                table: "FormValues");

            migrationBuilder.RenameColumn(
                name: "RecordId",
                newName: "ReceiptId",
                table: "ReceiptValues");

            migrationBuilder.RenameColumn(
                name: "RecordId",
                newName: "FormId",
                table: "FormValues");

            migrationBuilder.DropForeignKey(
                name: "FK_FormValues_Forms_RecordId",
                table: "FormValues");

            migrationBuilder.AddForeignKey(
                name: "FK_FormValues_Forms_FormId",
                table: "FormValues",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptValues_Receipts_RecordId",
                table: "ReceiptValues");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptValues_Receipts_ReceiptId",
                table: "ReceiptValues",
                column: "ReceiptId",
                principalTable: "Receipts",
                principalColumn: "Id");
        }
    }
}
