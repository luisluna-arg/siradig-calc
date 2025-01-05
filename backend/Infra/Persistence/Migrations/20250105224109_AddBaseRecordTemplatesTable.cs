using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infra.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseRecordTemplatesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "RecordTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordTemplates", x => x.Id);
                });

            migrationBuilder.Sql(@"
                INSERT INTO ""RecordTemplates"" (""Id"", ""CreatedAt"", ""UpdatedAt"")
                SELECT ""Id"", ""CreatedAt"", ""UpdatedAt"" FROM ""ReceiptTemplates""");

            migrationBuilder.Sql(@"
                INSERT INTO ""RecordTemplates"" (""Id"", ""CreatedAt"", ""UpdatedAt"")
                SELECT ""Id"", ""CreatedAt"", ""UpdatedAt"" FROM ""FormTemplates""");

            migrationBuilder.AddForeignKey(
                name: "FK_FormTemplates_RecordTemplates_Id",
                table: "FormTemplates",
                column: "Id",
                principalTable: "RecordTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptTemplates_RecordTemplates_Id",
                table: "ReceiptTemplates",
                column: "Id",
                principalTable: "RecordTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ReceiptTemplates");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ReceiptTemplates");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "FormTemplates");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "FormTemplates");            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ReceiptTemplates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ReceiptTemplates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "FormTemplates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "FormTemplates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.Sql(@"
                UPDATE ""ReceiptTemplates"" R
                    SET ""CreatedAt"" = REC.""CreatedAt"", ""UpdatedAt"" = REC.""UpdatedAt""
                FROM 
                    ""RecordTemplates"" REC
                WHERE R.""Id"" = REC.""Id""");

            migrationBuilder.Sql(@"
                UPDATE ""FormTemplates"" F
                    SET ""CreatedAt"" = REC.""CreatedAt"", ""UpdatedAt"" = REC.""UpdatedAt""
                FROM 
                    ""RecordTemplates"" REC
                WHERE F.""Id"" = REC.""Id""");

            migrationBuilder.DropForeignKey(
                name: "FK_FormTemplates_RecordTemplates_Id",
                table: "FormTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptTemplates_RecordTemplates_Id",
                table: "ReceiptTemplates");

            migrationBuilder.DropTable(
                name: "RecordTemplates");            
        }
    }
}
