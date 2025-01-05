using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infra.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseRecordsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Records",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Records", x => x.Id);
                });

            migrationBuilder.Sql(@"
                INSERT INTO ""Records"" (""Id"", ""CreatedAt"", ""UpdatedAt"")
                SELECT ""Id"", ""CreatedAt"", ""UpdatedAt"" FROM ""Receipts""");

            migrationBuilder.Sql(@"
                INSERT INTO ""Records"" (""Id"", ""CreatedAt"", ""UpdatedAt"")
                SELECT ""Id"", ""CreatedAt"", ""UpdatedAt"" FROM ""Forms""");


            migrationBuilder.AddForeignKey(
                name: "FK_Forms_Records_Id",
                table: "Forms",
                column: "Id",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Records_Id",
                table: "Receipts",
                column: "Id",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Forms");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Receipts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Receipts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Forms",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Forms",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.Sql(@"
                UPDATE ""Forms"" F
                    SET ""CreatedAt"" = REC.""CreatedAt"", ""UpdatedAt"" = REC.""UpdatedAt""
                FROM 
                    ""Records"" REC
                WHERE F.""Id"" = REC.""Id""");

            migrationBuilder.Sql(@"
                UPDATE ""Receipts"" R
                    SET ""CreatedAt"" = REC.""CreatedAt"", ""UpdatedAt"" = REC.""UpdatedAt""
                FROM 
                    ""Records"" REC
                WHERE R.""Id"" = REC.""Id""");

            migrationBuilder.DropForeignKey(
                name: "FK_Forms_Records_Id",
                table: "Forms");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Records_Id",
                table: "Receipts");

            migrationBuilder.DropTable(
                name: "Records");
        }
    }
}
