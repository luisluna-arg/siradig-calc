using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infra.Migrations
{
    /// <inheritdoc />
    public partial class Temp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecordTemplateFields_RecordTemplateSections_RecordTemplateS~",
                table: "RecordTemplateFields");

            migrationBuilder.AlterColumn<Guid>(
                name: "RecordTemplateSectionId",
                table: "RecordTemplateFields",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_RecordTemplateFields_RecordTemplateSections_RecordTemplateS~",
                table: "RecordTemplateFields",
                column: "RecordTemplateSectionId",
                principalTable: "RecordTemplateSections",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecordTemplateFields_RecordTemplateSections_RecordTemplateS~",
                table: "RecordTemplateFields");

            migrationBuilder.AlterColumn<Guid>(
                name: "RecordTemplateSectionId",
                table: "RecordTemplateFields",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RecordTemplateFields_RecordTemplateSections_RecordTemplateS~",
                table: "RecordTemplateFields",
                column: "RecordTemplateSectionId",
                principalTable: "RecordTemplateSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
