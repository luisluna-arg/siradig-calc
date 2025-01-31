using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

#nullable disable

namespace infra.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModelsRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            Up_DropForeignKeys(migrationBuilder);

            Up_RenameColumns(migrationBuilder);

            Up_RenameIndexes(migrationBuilder);

            Up_AddColumns(migrationBuilder);

            Up_CreateTable_RecordTemplateConversions(migrationBuilder);

            Up_CreateTable_RecordTemplateSections(migrationBuilder);

            Up_CreateTable_RecordTemplateFields(migrationBuilder);

            Up_CreateTable_RecordValues(migrationBuilder);

            Up_CreateIndexes(migrationBuilder);

            Up_AddForeignKeys(migrationBuilder);

            DropTables(migrationBuilder);
        }

        private void DropTables(MigrationBuilder migrationBuilder)
        {
            var tableNames = new string[]
            {
                "FormValues",
                "ReceiptToFormConversions",
                "ReceiptValues",
                "FormFields",
                "Forms",
                "ReceiptFields",
                "Receipts",
                "FormTemplateSection",
                "ReceiptTemplateSection",
                "FormTemplates",
                "ReceiptTemplates"
            };

            foreach (var tableName in tableNames)
            {
                DropTable(migrationBuilder, tableName);
            }
        }

        private void Up_DropForeignKeys(MigrationBuilder migrationBuilder)
        {
            var foreignKeys = new[]
            {
                ("FK_RecordFieldLinks_FormFields_FormFieldId", "RecordFieldLinks"),
                ("FK_RecordFieldLinks_ReceiptFields_ReceiptFieldId", "RecordFieldLinks"),
                ("FK_RecordTemplateLinks_FormTemplates_FormTemplateId", "RecordTemplateLinks"),
                ("FK_RecordTemplateLinks_ReceiptTemplates_ReceiptTemplateId", "RecordTemplateLinks"),
            };

            foreach (var foreignKey in foreignKeys)
            {
                DropForeignKey(migrationBuilder, foreignKey.Item1, foreignKey.Item2);
            }
        }

        private void Up_RenameColumns(MigrationBuilder migrationBuilder)
        {
            var columnNames = new[]
            {
                ("ReceiptTemplateId", "RecordTemplateLinks", "RightTemplateId"),
                ("FormTemplateId", "RecordTemplateLinks", "LeftTemplateId"),
                ("ReceiptFieldId", "RecordFieldLinks", "RightFieldId"),
                ("FormFieldId", "RecordFieldLinks", "LeftFieldId")
            };

            foreach (var column in columnNames)
            {
                RenameColumn(migrationBuilder, column.Item2, column.Item1, column.Item3);
            }
        }

        private void Up_RenameIndexes(MigrationBuilder migrationBuilder)
        {
            var indexName = new[]
            {
                ("IX_RecordTemplateLinks_ReceiptTemplateId", "RecordTemplateLinks", "IX_RecordTemplateLinks_RightTemplateId"),
                ("IX_RecordTemplateLinks_FormTemplateId", "RecordTemplateLinks", "IX_RecordTemplateLinks_LeftTemplateId"),
                ("IX_RecordFieldLinks_ReceiptFieldId", "RecordFieldLinks", "IX_RecordFieldLinks_RightFieldId"),
                ("IX_RecordFieldLinks_FormFieldId", "RecordFieldLinks", "IX_RecordFieldLinks_LeftFieldId")
            };

            foreach (var item in indexName)
            {
                RenameColumn(migrationBuilder, item.Item2, item.Item1, item.Item3);
            }
        }

        private void Up_AddColumns(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RecordTemplates",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "RecordTemplates",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "TemplateId",
                table: "Records",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Records",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "RecordTemplateFieldId",
                table: "RecordFieldLinks",
                type: "uuid",
                nullable: true);
        }

        private void Up_CreateTable_RecordTemplateConversions(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecordTemplateConversions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RecordTemplateLinkId = table.Column<Guid>(type: "uuid", nullable: false),
                    SourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                });

            var query = @"
                INSERT INTO ""RecordTemplateConversions"" (
                    ""Id"",
                    ""RecordTemplateLinkId"",
                    ""SourceId"",
                    ""TargetId"",
                    ""RecordId"",
                    ""CreatedAt"",
                    ""UpdatedAt"",
                    ""Deleted""
                )
                SELECT 
                    ""Id"",
                    ""RecordTemplateLinkId"",
                    ""SourceId"",
                    ""TargetId"",
                    ""CreatedAt"",
                    ""UpdatedAt"",
                    ""Deleted"" 
                FROM 
                    ""ReceiptToFormConversions""
                ";

            migrationBuilder.Sql(query);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecordTemplateConversions",
                table: "RecordTemplateConversions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecordTemplateConversions_RecordTemplateLinks_RecordTemplat~",
                table: "RecordTemplateConversions",
                column: "RecordTemplateLinkId",
                principalTable: "RecordTemplateLinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecordTemplateConversions_Records_SourceId",
                table: "RecordTemplateConversions",
                column: "SourceId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecordTemplateConversions_Records_TargetId",
                table: "RecordTemplateConversions",
                column: "TargetId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        private void Up_CreateTable_RecordTemplateSections(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecordTemplateSections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    RecordTemplateId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                });

            var query = @"
                INSERT INTO ""RecordTemplateConversions"" (
                    ""Id"", ""Name"", ""RecordTemplateId"", ""CreatedAt"", ""UpdatedAt"", ""Deleted""
                )
                SELECT * FROM (
                    SELECT ""Id"", ""ReceiptTemplateId"" AS ""RecordTemplateId"", ""CreatedAt"", ""UpdatedAt"", ""Name"", ""Deleted""
                    FROM ""ReceiptTemplateSection""
                    UNION
                    SELECT ""Id"", ""FormTemplateId"" AS ""RecordTemplateId"", ""CreatedAt"", ""UpdatedAt"", ""Name"", ""Deleted""
                    FROM ""FormTemplateSection""
                )";

            migrationBuilder.Sql(query);

            AddPrimaryKey(migrationBuilder, "PK_RecordTemplateSections", "RecordTemplateSections", "Id");

            AddForeignKey(
                migrationBuilder,
                "FK_RecordTemplateSections_RecordTemplates_RecordTemplateId",
                "RecordTemplateSections",
                "RecordTemplateId",
                "RecordTemplates",
                "Id");
        }

        private void Up_CreateTable_RecordTemplateFields(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecordTemplateFields",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RecordTemplateSectionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Label = table.Column<string>(type: "text", nullable: false),
                    FieldType = table.Column<int>(type: "integer", nullable: false),
                    IsRequired = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                });

            var query = @"
                INSERT INTO ""RecordTemplateConversions"" (
                    ""Id"", ""RecordTemplateSectionId"", ""Label"", ""FieldType"", 
                    ""IsRequired"", ""CreatedAt"", ""UpdatedAt"", ""Deleted"",
                )
                SELECT * FROM (
                    SELECT 
                        ""Id"", ""FormTemplateSectionId"" AS ""TemplateSectionId"", ""CreatedAt"", ""UpdatedAt"",
                        ""Label"", ""FieldType"", ""IsRequired"", ""Deleted""
                    FROM ""FormFields""
                    UNION
                    SELECT 
                        ""Id"", ""ReceiptTemplateSectionId"" AS ""TemplateSectionId"", ""CreatedAt"", ""UpdatedAt"",
                        ""Label"", ""FieldType"", ""IsRequired"", ""Deleted""
                    FROM ""ReceiptFields""
                )";

            migrationBuilder.Sql(query);

            AddPrimaryKey(migrationBuilder, "PK_RecordTemplateFields", "RecordTemplateFields", "Id");

            AddForeignKey(
                migrationBuilder,
                "FK_RecordTemplateFields_RecordTemplateSections_RecordTemplateS~",
                "RecordTemplateFields",
                "RecordTemplateSectionId",
                "RecordTemplateSections",
                "Id",
                onDelete: ReferentialAction.Cascade);
        }

        private void Up_CreateTable_RecordValues(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecordValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RecordId = table.Column<Guid>(type: "uuid", nullable: false),
                    FieldId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                });

            var sql = @"
                SELECT
                    ""Id"", ""RecordId"", ""FieldId"", ""Value"", ""CreatedAt"", ""UpdatedAt"", ""Deleted""
                FROM (
                    SELECT 
                        ""Id"", ""RecordId"", ""FieldId"", ""Value"", ""CreatedAt"", ""UpdatedAt"", ""Deleted""
                    FROM ""ReceiptValues""
                    UNION
                    SELECT 
                        ""Id"", ""RecordId"", ""FieldId"", ""Value"", ""CreatedAt"", ""UpdatedAt"", ""Deleted""
                    FROM ""FormValues""
                )
            ";

            migrationBuilder.Sql(sql);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecordValues",
                table: "RecordValues",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecordValues_RecordTemplateFields_FieldId",
                table: "RecordValues",
                column: "FieldId",
                principalTable: "RecordTemplateFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecordValues_Records_RecordId",
                table: "RecordValues",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        private void Up_CreateIndexes(MigrationBuilder migrationBuilder)
        {
            var indexes = new[]
            {
                ("IX_Records_TemplateId", "Records", "TemplateId"),
                ("IX_RecordFieldLinks_RecordTemplateFieldId", "RecordFieldLinks", "RecordTemplateFieldId"),
                ("IX_RecordTemplateConversions_RecordTemplateLinkId", "RecordTemplateConversions", "RecordTemplateLinkId"),
                ("IX_RecordTemplateConversions_SourceId", "RecordTemplateConversions", "SourceId"),
                ("IX_RecordTemplateConversions_TargetId", "RecordTemplateConversions", "TargetId"),
                ("IX_RecordTemplateFields_RecordTemplateSectionId", "RecordTemplateConversions", "RecordTemplateSectionId"),
                ("IX_RecordTemplateSections_RecordTemplateId", "RecordTemplateSections", "RecordTemplateId"),
                ("IX_RecordValues_FieldId", "RecordValues", "FieldId"),
                ("IX_RecordValues_RecordId", "RecordValues", "RecordId")
            };

            foreach (var index in indexes)
            {
                CreateIndex(migrationBuilder, index.Item2, index.Item1, index.Item3);
            }
        }

        private void Up_AddForeignKeys(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_RecordFieldLinks_RecordTemplateFields_LeftFieldId",
                table: "RecordFieldLinks",
                column: "LeftFieldId",
                principalTable: "RecordTemplateFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecordFieldLinks_RecordTemplateFields_RecordTemplateFieldId",
                table: "RecordFieldLinks",
                column: "RecordTemplateFieldId",
                principalTable: "RecordTemplateFields",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecordFieldLinks_RecordTemplateFields_RightFieldId",
                table: "RecordFieldLinks",
                column: "RightFieldId",
                principalTable: "RecordTemplateFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_RecordTemplates_TemplateId",
                table: "Records",
                column: "TemplateId",
                principalTable: "RecordTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecordTemplateLinks_RecordTemplates_LeftTemplateId",
                table: "RecordTemplateLinks",
                column: "LeftTemplateId",
                principalTable: "RecordTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecordTemplateLinks_RecordTemplates_RightTemplateId",
                table: "RecordTemplateLinks",
                column: "RightTemplateId",
                principalTable: "RecordTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            Down_DropForeignKeys(migrationBuilder);

            Down_DropIndexes(migrationBuilder);

            Down_DropColumns(migrationBuilder);

            Down_RenameColumns(migrationBuilder);

            Down_RenameIndexes(migrationBuilder);

            migrationBuilder.CreateTable(
                name: "FormTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormTemplates_RecordTemplates_Id",
                        column: x => x.Id,
                        principalTable: "RecordTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptTemplates_RecordTemplates_Id",
                        column: x => x.Id,
                        principalTable: "RecordTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Forms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RecordTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Forms_FormTemplates_RecordTemplateId",
                        column: x => x.RecordTemplateId,
                        principalTable: "FormTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Forms_Records_Id",
                        column: x => x.Id,
                        principalTable: "Records",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormTemplateSection",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    FormTemplateId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormTemplateSection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormTemplateSection_FormTemplates_FormTemplateId",
                        column: x => x.FormTemplateId,
                        principalTable: "FormTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RecordTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receipts_ReceiptTemplates_RecordTemplateId",
                        column: x => x.RecordTemplateId,
                        principalTable: "ReceiptTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receipts_Records_Id",
                        column: x => x.Id,
                        principalTable: "Records",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptTemplateSection",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ReceiptTemplateId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptTemplateSection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptTemplateSection_ReceiptTemplates_ReceiptTemplateId",
                        column: x => x.ReceiptTemplateId,
                        principalTable: "ReceiptTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FormFields",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    FieldType = table.Column<int>(type: "integer", nullable: false),
                    FormTemplateSectionId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsRequired = table.Column<bool>(type: "boolean", nullable: false),
                    Label = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormFields_FormTemplateSection_FormTemplateSectionId",
                        column: x => x.FormTemplateSectionId,
                        principalTable: "FormTemplateSection",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReceiptToFormConversions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RecordTemplateLinkId = table.Column<Guid>(type: "uuid", nullable: false),
                    SourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "ReceiptFields",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    FieldType = table.Column<int>(type: "integer", nullable: false),
                    IsRequired = table.Column<bool>(type: "boolean", nullable: false),
                    Label = table.Column<string>(type: "text", nullable: false),
                    ReceiptTemplateSectionId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptFields_ReceiptTemplateSection_ReceiptTemplateSection~",
                        column: x => x.ReceiptTemplateSectionId,
                        principalTable: "ReceiptTemplateSection",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FormValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FieldId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecordId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormValues_FormFields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "FormFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FormValues_Forms_RecordId",
                        column: x => x.RecordId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FieldId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecordId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptValues_ReceiptFields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "ReceiptFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptValues_Receipts_RecordId",
                        column: x => x.RecordId,
                        principalTable: "Receipts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            Down_CreateIndexes(migrationBuilder);

            Down_AddForeignKeys(migrationBuilder);

            Down_DropTables(migrationBuilder);
        }

        private void Down_DropForeignKeys(MigrationBuilder migrationBuilder)
        {
            var foreignKeys = new[]
            {
                ("FK_RecordFieldLinks_RecordTemplateFields_LeftFieldId", "RecordFieldLinks"),
                ("FK_RecordFieldLinks_RecordTemplateFields_RecordTemplateFieldId", "RecordFieldLinks"),
                ("FK_RecordFieldLinks_RecordTemplateFields_RightFieldId", "RecordFieldLinks"),
                ("FK_Records_RecordTemplates_TemplateId", "Records"),
                ("FK_RecordTemplateLinks_RecordTemplates_LeftTemplateId", "RecordTemplateLinks"),
                ("FK_RecordTemplateLinks_RecordTemplates_RightTemplateId", "RecordTemplateLinks"),
            };

            foreach (var foreignKey in foreignKeys)
            {
                DropForeignKey(migrationBuilder, foreignKey.Item1, foreignKey.Item2);
            }
        }

        private void Down_DropIndexes(MigrationBuilder migrationBuilder)
        {
            DropIndex(migrationBuilder, "IX_Records_TemplateId", "Records");

            DropIndex(migrationBuilder, "IX_RecordFieldLinks_RecordTemplateFieldId", "RecordFieldLinks");
        }

        private void Down_DropColumns(MigrationBuilder migrationBuilder)
        {
            var columns = new[]
            {
                ("Description", "RecordTemplates"),
                ("Name", "RecordTemplates"),
                ("TemplateId", "Records"),
                ("Title", "Records"),
                ("RecordTemplateFieldId", "RecordFieldLinks")
            };

            foreach (var column in columns)
            {
                DropColumn(migrationBuilder, column.Item1, column.Item2);
            }
        }

        private void Down_RenameColumns(MigrationBuilder migrationBuilder)
        {
            var columns = new[]
            {
                ("RightTemplateId", "RecordTemplateLinks","ReceiptTemplateId"),
                ("LeftTemplateId", "RecordTemplateLinks", "FormTemplateId"),
                ("RightFieldId", "RecordFieldLinks", "ReceiptFieldId"),
                ("LeftFieldId", "RecordFieldLinks", "FormFieldId")
            };

            foreach (var column in columns)
            {
                RenameColumn(migrationBuilder, column.Item1, column.Item2, column.Item3);
            }
        }

        private void Down_RenameIndexes(MigrationBuilder migrationBuilder)
        {
            var indexes = new[]
            {
                ("IX_RecordTemplateLinks_RightTemplateId", "RecordTemplateLinks", "IX_RecordTemplateLinks_ReceiptTemplateId"),
                ("IX_RecordTemplateLinks_LeftTemplateId", "RecordTemplateLinks", "IX_RecordTemplateLinks_FormTemplateId"),
                ("IX_RecordFieldLinks_RightFieldId", "RecordFieldLinks", "IX_RecordFieldLinks_ReceiptFieldId"),
                ("IX_RecordFieldLinks_LeftFieldId", "RecordFieldLinks", "IX_RecordFieldLinks_FormFieldId"),
            };

            foreach (var index in indexes)
            {
                RenameIndex(migrationBuilder, index.Item1, index.Item2, index.Item3);
            }
        }

        private void Down_DropTables(MigrationBuilder migrationBuilder)
        {
            var tableNames = new[]
            {
                "RecordTemplateConversions",
                "RecordValues",
                "RecordTemplateFields",
                "RecordTemplateSections"
            };

            foreach (var tableName in tableNames)
            {
                DropTable(migrationBuilder, tableName);
            }
        }

        private void Down_CreateIndexes(MigrationBuilder migrationBuilder)
        {
            var indexes = new[] {
                ("IX_FormFields_FormTemplateSectionId", "FormFields", "FormTemplateSectionId"),
                ("IX_Forms_RecordTemplateId", "Forms", "RecordTemplateId"),
                ("IX_FormTemplateSection_FormTemplateId", "FormTemplateSection", "FormTemplateId"),
                ("IX_FormValues_FieldId", "FormValues", "FieldId"),
                ("IX_FormValues_RecordId", "FormValues", "RecordId"),
                ("IX_ReceiptFields_ReceiptTemplateSectionId", "ReceiptFields", "ReceiptTemplateSectionId"),
                ("IX_Receipts_RecordTemplateId", "Receipts", "RecordTemplateId"),
                ("IX_ReceiptTemplateSection_ReceiptTemplateId", "ReceiptTemplateSection", "ReceiptTemplateId"),
                ("IX_ReceiptToFormConversions_RecordTemplateLinkId", "ReceiptToFormConversions", "RecordTemplateLinkId"),
                ("IX_ReceiptToFormConversions_SourceId", "ReceiptToFormConversions", "SourceId"),
                ("IX_ReceiptToFormConversions_TargetId", "ReceiptToFormConversions", "TargetId"),
                ("IX_ReceiptValues_FieldId", "ReceiptValues", "FieldId"),
                ("IX_ReceiptValues_RecordId", "ReceiptValues", "RecordId"),
            };

            foreach (var index in indexes)
            {
                CreateIndex(migrationBuilder, index.Item1, index.Item2, index.Item3);
            }
        }

        private void Down_AddForeignKeys(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_RecordFieldLinks_FormFields_FormFieldId",
                table: "RecordFieldLinks",
                column: "FormFieldId",
                principalTable: "FormFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecordFieldLinks_ReceiptFields_ReceiptFieldId",
                table: "RecordFieldLinks",
                column: "ReceiptFieldId",
                principalTable: "ReceiptFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecordTemplateLinks_FormTemplates_FormTemplateId",
                table: "RecordTemplateLinks",
                column: "FormTemplateId",
                principalTable: "FormTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecordTemplateLinks_ReceiptTemplates_ReceiptTemplateId",
                table: "RecordTemplateLinks",
                column: "ReceiptTemplateId",
                principalTable: "ReceiptTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        // Common methods

        private void AddPrimaryKey(MigrationBuilder migrationBuilder, string name, string table, string column)
            => migrationBuilder.AddPrimaryKey(name: name, table: table, column: column);

        private void AddForeignKey(MigrationBuilder migrationBuilder, string name, string table, string column, string principalTable, string principalColumn, ReferentialAction? onDelete = null)
            => migrationBuilder.AddForeignKey(
                name: name,
                table: table,
                column: column,
                principalTable: principalTable,
                principalColumn: principalColumn,
                onDelete: onDelete ?? ReferentialAction.NoAction);

        private void CreateIndex(MigrationBuilder migrationBuilder, string table, string name, string column)
            => migrationBuilder.CreateIndex(name: name, table: table, column: column);

        private void RenameIndex(MigrationBuilder migrationBuilder, string name, string table, string newName)
            => migrationBuilder.RenameIndex(name: name, table: table, newName: newName);

        private void RenameColumn(MigrationBuilder migrationBuilder, string tableName, string name, string newName)
            => migrationBuilder.RenameColumn(name: name, table: tableName, newName: newName);

        private void DropForeignKey(MigrationBuilder migrationBuilder, string keyName, string tableName)
            => migrationBuilder.DropForeignKey(name: keyName, table: tableName);

        private void DropIndex(MigrationBuilder migrationBuilder, string name, string table)
            => migrationBuilder.DropIndex(name: name, table: table);

        private void DropTable(MigrationBuilder migrationBuilder, string tableName)
            => migrationBuilder.DropTable(name: tableName);

        private void DropColumn(MigrationBuilder migrationBuilder, string column, string tableName)
            => migrationBuilder.DropColumn(name: column, table: tableName);
    }
}
