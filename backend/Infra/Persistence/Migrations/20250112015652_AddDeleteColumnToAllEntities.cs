using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infra.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDeleteColumnToAllEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "RecordTemplates",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "RecordTemplateLinks",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Records",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "RecordFieldLinks",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "ReceiptValues",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "ReceiptToFormConversions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "ReceiptTemplateSection",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "ReceiptFields",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "FormValues",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "FormTemplateSection",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "FormFields",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "FieldTypeMappings",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "RecordTemplates");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "RecordTemplateLinks");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "RecordFieldLinks");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "ReceiptValues");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "ReceiptToFormConversions");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "ReceiptTemplateSection");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "ReceiptFields");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "FormValues");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "FormTemplateSection");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "FormFields");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "FieldTypeMappings");
        }
    }
}
