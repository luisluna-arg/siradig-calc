using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infra.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModelsRefactorOld : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            Up_DropForeignKeys(migrationBuilder);

            Up_RenameColumns(migrationBuilder);

            Up_RenameIndexes(migrationBuilder);

            Up_AddColumns(migrationBuilder);

            Up_CreateTable_RecordTemplateConversions(migrationBuilder);

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
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordTemplateSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordTemplateSections_RecordTemplates_RecordTemplateId",
                        column: x => x.RecordTemplateId,
                        principalTable: "RecordTemplates",
                        principalColumn: "Id");
                });

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
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordTemplateFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordTemplateFields_RecordTemplateSections_RecordTemplateS~",
                        column: x => x.RecordTemplateSectionId,
                        principalTable: "RecordTemplateSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordValues_RecordTemplateFields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "RecordTemplateFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecordValues_Records_RecordId",
                        column: x => x.RecordId,
                        principalTable: "Records",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Records_TemplateId",
                table: "Records",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordFieldLinks_RecordTemplateFieldId",
                table: "RecordFieldLinks",
                column: "RecordTemplateFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordTemplateConversions_RecordId",
                table: "RecordTemplateConversions",
                column: "RecordId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordTemplateConversions_RecordTemplateLinkId",
                table: "RecordTemplateConversions",
                column: "RecordTemplateLinkId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordTemplateConversions_SourceId",
                table: "RecordTemplateConversions",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordTemplateConversions_TargetId",
                table: "RecordTemplateConversions",
                column: "TargetId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordTemplateFields_RecordTemplateSectionId",
                table: "RecordTemplateFields",
                column: "RecordTemplateSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordTemplateSections_RecordTemplateId",
                table: "RecordTemplateSections",
                column: "RecordTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordValues_FieldId",
                table: "RecordValues",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordValues_RecordId",
                table: "RecordValues",
                column: "RecordId");

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

            DropTables(migrationBuilder);
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
                    RecordId = table.Column<Guid>(type: "uuid", nullable: true),
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
                name: "FK_RecordTemplateConversions_Records_RecordId",
                table: "RecordTemplateConversions",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecordTemplateConversions_Records_SourceId",
                table: "RecordTemplateConversions",
                column: "SourceId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecordTemplateConversions_Records_TargetId",
                table: "RecordTemplateConversions",
                column: "TargetId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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

        private void Up_RenameIndexes(MigrationBuilder migrationBuilder)
        {
            var indexName = new[]
            {
                ("IX_RecordTemplateLinks_ReceiptTemplateId", "IX_RecordTemplateLinks_LeftTemplateId"),
                ("IX_RecordTemplateLinks_FormTemplateId", "IX_RecordTemplateLinks_RightTemplateId")
            };

            foreach (var item in indexName)
            {
                RenameColumn(migrationBuilder, "RecordTemplateLinks", item.Item1, item.Item2);
            }

            indexName = new[]
            {
                ("IX_RecordFieldLinks_ReceiptFieldId", "IX_RecordFieldLinks_LeftFieldId"),
                ("IX_RecordFieldLinks_FormFieldId", "IX_RecordFieldLinks_RightFieldId")
            };

            foreach (var item in indexName)
            {
                RenameColumn(migrationBuilder, "RecordFieldLinks", item.Item1, item.Item2);
            }
        }

        private void Up_RenameColumns(MigrationBuilder migrationBuilder)
        {
            var columnNames = new[]
            {
                ("ReceiptTemplateId", "LeftTemplateId"),
                ("FormTemplateId", "RightTemplateId")
            };

            foreach (var item in columnNames)
            {
                RenameColumn(migrationBuilder, "RecordTemplateLinks", item.Item1, item.Item2);
            }

            columnNames = new[]
            {
                ("ReceiptFieldId", "LeftFieldId"),
                ("FormFieldId", "RightFieldId")
            };

            foreach (var item in columnNames)
            {
                RenameColumn(migrationBuilder, "RecordFieldLinks", item.Item1, item.Item2);
            }
        }

        private void Up_DropForeignKeys(MigrationBuilder migrationBuilder)
        {
            var keyNames = new string[]
            {
                "FK_RecordFieldLinks_FormFields_FormFieldId",
                "FK_RecordFieldLinks_ReceiptFields_ReceiptFieldId",
            };

            foreach (var keyName in keyNames)
            {
                DropForeignKey(migrationBuilder, keyName, "RecordFieldLinks");
            }

            keyNames = new[]
            {
                "FK_RecordTemplateLinks_FormTemplates_FormTemplateId",
                "FK_RecordTemplateLinks_ReceiptTemplates_ReceiptTemplateId",
            };

            foreach (var keyName in keyNames)
            {
                DropForeignKey(migrationBuilder, keyName, "RecordTemplateLinks");
            }
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
                migrationBuilder.DropTable(
                name: tableName);
            }

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecordFieldLinks_RecordTemplateFields_LeftFieldId",
                table: "RecordFieldLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_RecordFieldLinks_RecordTemplateFields_RecordTemplateFieldId",
                table: "RecordFieldLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_RecordFieldLinks_RecordTemplateFields_RightFieldId",
                table: "RecordFieldLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_RecordTemplates_TemplateId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_RecordTemplateLinks_RecordTemplates_LeftTemplateId",
                table: "RecordTemplateLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_RecordTemplateLinks_RecordTemplates_RightTemplateId",
                table: "RecordTemplateLinks");

            migrationBuilder.DropTable(
                name: "RecordTemplateConversions");

            migrationBuilder.DropTable(
                name: "RecordValues");

            migrationBuilder.DropTable(
                name: "RecordTemplateFields");

            migrationBuilder.DropTable(
                name: "RecordTemplateSections");

            migrationBuilder.DropIndex(
                name: "IX_Records_TemplateId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_RecordFieldLinks_RecordTemplateFieldId",
                table: "RecordFieldLinks");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "RecordTemplates");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "RecordTemplates");

            migrationBuilder.DropColumn(
                name: "TemplateId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "RecordTemplateFieldId",
                table: "RecordFieldLinks");

            migrationBuilder.RenameColumn(
                name: "RightTemplateId",
                table: "RecordTemplateLinks",
                newName: "ReceiptTemplateId");

            migrationBuilder.RenameColumn(
                name: "LeftTemplateId",
                table: "RecordTemplateLinks",
                newName: "FormTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_RecordTemplateLinks_RightTemplateId",
                table: "RecordTemplateLinks",
                newName: "IX_RecordTemplateLinks_ReceiptTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_RecordTemplateLinks_LeftTemplateId",
                table: "RecordTemplateLinks",
                newName: "IX_RecordTemplateLinks_FormTemplateId");

            migrationBuilder.RenameColumn(
                name: "RightFieldId",
                table: "RecordFieldLinks",
                newName: "ReceiptFieldId");

            migrationBuilder.RenameColumn(
                name: "LeftFieldId",
                table: "RecordFieldLinks",
                newName: "FormFieldId");

            migrationBuilder.RenameIndex(
                name: "IX_RecordFieldLinks_RightFieldId",
                table: "RecordFieldLinks",
                newName: "IX_RecordFieldLinks_ReceiptFieldId");

            migrationBuilder.RenameIndex(
                name: "IX_RecordFieldLinks_LeftFieldId",
                table: "RecordFieldLinks",
                newName: "IX_RecordFieldLinks_FormFieldId");

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

            migrationBuilder.CreateIndex(
                name: "IX_FormFields_FormTemplateSectionId",
                table: "FormFields",
                column: "FormTemplateSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Forms_RecordTemplateId",
                table: "Forms",
                column: "RecordTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_FormTemplateSection_FormTemplateId",
                table: "FormTemplateSection",
                column: "FormTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_FormValues_FieldId",
                table: "FormValues",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_FormValues_RecordId",
                table: "FormValues",
                column: "RecordId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptFields_ReceiptTemplateSectionId",
                table: "ReceiptFields",
                column: "ReceiptTemplateSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_RecordTemplateId",
                table: "Receipts",
                column: "RecordTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTemplateSection_ReceiptTemplateId",
                table: "ReceiptTemplateSection",
                column: "ReceiptTemplateId");

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

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptValues_FieldId",
                table: "ReceiptValues",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptValues_RecordId",
                table: "ReceiptValues",
                column: "RecordId");

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



        private void RenameIndex(MigrationBuilder migrationBuilder, string table, string name, string newName)
        {
            migrationBuilder.RenameIndex(
                name: name,
                table: table,
                newName: newName);
        }

        private void RenameColumn(MigrationBuilder migrationBuilder, string tableName, string name, string newName)
        {
            migrationBuilder.RenameColumn(
                name: name,
                table: tableName,
                newName: newName);
        }

        private void DropForeignKey(MigrationBuilder migrationBuilder, string keyName, string tableName)
        {
            migrationBuilder.DropForeignKey(
                name: keyName,
                table: tableName);
        }
    }
}
