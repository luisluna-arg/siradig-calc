using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infra.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMappingBetweenTemplateAndFieldLinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TemplateLinkId",
                table: "RecordFieldLinks",
                type: "uuid",
                nullable: true);

            migrationBuilder.Sql(@"
                UPDATE ""RecordFieldLinks"" RFL
                    SET ""TemplateLinkId"" = RTL.""Id""
                FROM 
                    ""RecordFieldLinks"" RFL1
                    inner JOIN ""FormFields"" FF ON FF.""Id"" = RFL1.""FormFieldId""
                    INNER JOIN ""FormTemplateSection"" FTS ON FTS.""Id"" = FF.""FormTemplateSectionId""
                    inner JOIN ""ReceiptFields"" RF ON RF.""Id"" = RFL1.""ReceiptFieldId""
                    INNER JOIN ""ReceiptTemplateSection"" RTS ON RTS.""Id"" = RF.""ReceiptTemplateSectionId""
                    INNER JOIN ""RecordTemplateLinks"" RTL ON RTL.""FormTemplateId"" = FTS.""FormTemplateId"" AND RTL.""ReceiptTemplateId"" = RTS.""ReceiptTemplateId""
                WHERE RFL.""Id"" = RFL1.""Id""");

            migrationBuilder.AlterColumn<Guid>(
                name: "TemplateLinkId",
                table: "RecordFieldLinks",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_RecordFieldLinks_TemplateLinkId",
                table: "RecordFieldLinks",
                column: "TemplateLinkId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecordFieldLinks_RecordTemplateLinks_TemplateLinkId",
                table: "RecordFieldLinks",
                column: "TemplateLinkId",
                principalTable: "RecordTemplateLinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecordFieldLinks_RecordTemplateLinks_TemplateLinkId",
                table: "RecordFieldLinks");

            migrationBuilder.DropIndex(
                name: "IX_RecordFieldLinks_TemplateLinkId",
                table: "RecordFieldLinks");

            migrationBuilder.DropColumn(
                name: "TemplateLinkId",
                table: "RecordFieldLinks");
        }
    }
}
