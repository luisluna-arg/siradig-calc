using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infra.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenamedDataContainerToRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "DataContainerFieldLinks",
                newName: "RecordFieldLinks");

            migrationBuilder.RenameTable(
                name: "DataContainerLinks",
                newName: "RecordTemplateLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_Forms_FormTemplates_DataContainerId",
                table: "Forms");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_ReceiptTemplates_DataContainerId",
                table: "Receipts");

            migrationBuilder.RenameColumn(
                name: "DataContainerId",
                table: "Receipts",
                newName: "RecordId");

            migrationBuilder.RenameIndex(
                name: "IX_Receipts_DataContainerId",
                table: "Receipts",
                newName: "IX_Receipts_RecordId");

            migrationBuilder.RenameColumn(
                name: "DataContainerId",
                table: "Forms",
                newName: "RecordId");

            migrationBuilder.RenameIndex(
                name: "IX_Forms_DataContainerId",
                table: "Forms",
                newName: "IX_Forms_RecordId");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataContainerFieldLinks",
                table: "RecordFieldLinks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecordFieldLinks",
                table: "RecordFieldLinks",
                column: "Id");

            migrationBuilder.DropForeignKey(
                name: "FK_DataContainerFieldLinks_FormFields_FormFieldId",
                table: "RecordFieldLinks");

            migrationBuilder.AddForeignKey(
                name: "FK_RecordFieldLinks_FormFields_FormFieldId",
                table: "RecordFieldLinks",
                column: "FormFieldId",
                principalTable: "FormFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            
            migrationBuilder.DropForeignKey(
                name: "FK_DataContainerFieldLinks_ReceiptFields_ReceiptFieldId",
                table: "RecordFieldLinks");

            migrationBuilder.AddForeignKey(
                name: "FK_RecordFieldLinks_ReceiptFields_ReceiptFieldId",
                table: "RecordFieldLinks",
                column: "ReceiptFieldId",
                principalTable: "ReceiptFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataContainerLinks",
                table: "RecordTemplateLinks" 
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecordTemplateLinks",
                table: "RecordTemplateLinks",
                column: "Id");

            migrationBuilder.DropForeignKey(
                name: "FK_DataContainerLinks_FormTemplates_FormTemplateId",
                table: "RecordTemplateLinks"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_RecordTemplateLinks_FormTemplates_FormTemplateId",
                table: "RecordTemplateLinks",
                column: "FormTemplateId",
                principalTable: "FormTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropForeignKey(
                name: "FK_DataContainerLinks_ReceiptTemplates_ReceiptTemplateId",
                table: "RecordTemplateLinks"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_RecordTemplateLinks_ReceiptTemplates_ReceiptTemplateId",
                table: "RecordTemplateLinks",
                column: "ReceiptTemplateId",
                principalTable: "ReceiptTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.RenameIndex(
                name: "IX_DataContainerFieldLinks_FormFieldId",
                newName: "IX_RecordFieldLinks_FormFieldId",
                table: "RecordFieldLinks");

            migrationBuilder.RenameIndex(
                name: "IX_DataContainerFieldLinks_ReceiptFieldId",
                newName: "IX_RecordFieldLinks_ReceiptFieldId",
                table: "RecordFieldLinks");

            migrationBuilder.RenameIndex(
                name: "IX_DataContainerLinks_FormTemplateId",
                newName: "IX_RecordTemplateLinks_FormTemplateId",
                table: "RecordTemplateLinks");

            migrationBuilder.RenameIndex(
                name: "IX_DataContainerLinks_ReceiptTemplateId",
                newName: "IX_RecordTemplateLinks_ReceiptTemplateId",
                table: "RecordTemplateLinks");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "RecordTemplateLinks",
                newName: "DataContainerLinks");

            migrationBuilder.RenameTable(
                name: "RecordFieldLinks",
                newName: "DataContainerFieldLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_Forms_FormTemplates_RecordId",
                table: "Forms");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_ReceiptTemplates_RecordId",
                table: "Receipts");

            migrationBuilder.RenameColumn(
                name: "RecordId",
                table: "Receipts",
                newName: "DataContainerId");

            migrationBuilder.RenameIndex(
                name: "IX_Receipts_RecordId",
                table: "Receipts",
                newName: "IX_Receipts_DataContainerId");

            migrationBuilder.RenameColumn(
                name: "RecordId",
                table: "Forms",
                newName: "DataContainerId");

            migrationBuilder.RenameIndex(
                name: "IX_Forms_RecordId",
                table: "Forms",
                newName: "IX_Forms_DataContainerId");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecordFieldLinks",
                table: "DataContainerFieldLinks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataContainerFieldLinks",
                table: "DataContainerFieldLinks",
                column: "Id");

            migrationBuilder.DropForeignKey(
                name: "FK_RecordFieldLinks_FormFields_FormFieldId",
                table: "DataContainerFieldLinks"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_DataContainerFieldLinks_FormFields_FormFieldId",
                table: "DataContainerFieldLinks",
                column: "FormFieldId",
                principalTable: "FormFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropForeignKey(
                name: "FK_RecordFieldLinks_ReceiptFields_ReceiptFieldId",
                table: "DataContainerFieldLinks"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_DataContainerFieldLinks_ReceiptFields_ReceiptFieldId",
                table: "DataContainerFieldLinks",
                column: "ReceiptFieldId",
                principalTable: "ReceiptFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecordTemplateLinks",
                table: "DataContainerLinks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataContainerLinks",
                table: "DataContainerLinks",
                column: "Id");

            migrationBuilder.DropForeignKey(
                name: "FK_RecordTemplateLinks_FormTemplates_FormTemplateId",
                table: "DataContainerLinks"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_DataContainerLinks_FormTemplates_FormTemplateId",
                table: "DataContainerLinks",
                column: "FormTemplateId",
                principalTable: "FormTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropForeignKey(
                name: "FK_RecordTemplateLinks_ReceiptTemplates_ReceiptTemplateId",
                table: "DataContainerLinks"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_DataContainerLinks_ReceiptTemplates_ReceiptTemplateId",
                table: "DataContainerLinks",
                column: "ReceiptTemplateId",
                principalTable: "ReceiptTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.RenameIndex(
                name: "IX_RecordFieldLinks_FormFieldId",
                newName: "IX_DataContainerFieldLinks_FormFieldId",
                table: "DataContainerFieldLinks");

            migrationBuilder.RenameIndex(
                name: "IX_RecordFieldLinks_ReceiptFieldId",
                newName: "IX_DataContainerFieldLinks_ReceiptFieldId",
                table: "DataContainerFieldLinks");

            migrationBuilder.RenameIndex(
                name: "IX_RecordTemplateLinks_FormTemplateId",
                newName: "IX_DataContainerLinks_FormTemplateId",
                table: "DataContainerLinks");

            migrationBuilder.RenameIndex(
                name: "IX_RecordTemplateLinks_ReceiptTemplateId",
                newName: "IX_DataContainerLinks_ReceiptTemplateId",
                table: "DataContainerLinks");

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_FormTemplates_DataContainerId",
                table: "Forms",
                column: "DataContainerId",
                principalTable: "FormTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_ReceiptTemplates_DataContainerId",
                table: "Receipts",
                column: "DataContainerId",
                principalTable: "ReceiptTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
