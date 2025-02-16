using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace infra.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChangeLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityName = table.Column<string>(type: "text", nullable: false),
                    Action = table.Column<string>(type: "text", nullable: false),
                    PrimaryKey = table.Column<string>(type: "text", nullable: false),
                    Changes = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    User = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FieldTypeMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldTypeMappings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecordTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Records",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Records", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Records_RecordTemplates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "RecordTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecordTemplateLinks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RightTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    LeftTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordTemplateLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordTemplateLinks_RecordTemplates_LeftTemplateId",
                        column: x => x.LeftTemplateId,
                        principalTable: "RecordTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecordTemplateLinks_RecordTemplates_RightTemplateId",
                        column: x => x.RightTemplateId,
                        principalTable: "RecordTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordTemplateConversions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordTemplateConversions_RecordTemplateLinks_RecordTemplat~",
                        column: x => x.RecordTemplateLinkId,
                        principalTable: "RecordTemplateLinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecordTemplateConversions_Records_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Records",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecordTemplateConversions_Records_TargetId",
                        column: x => x.TargetId,
                        principalTable: "Records",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "RecordFieldLinks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TemplateLinkId = table.Column<Guid>(type: "uuid", nullable: false),
                    RightFieldId = table.Column<Guid>(type: "uuid", nullable: false),
                    LeftFieldId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecordTemplateFieldId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordFieldLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordFieldLinks_RecordTemplateFields_LeftFieldId",
                        column: x => x.LeftFieldId,
                        principalTable: "RecordTemplateFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecordFieldLinks_RecordTemplateFields_RecordTemplateFieldId",
                        column: x => x.RecordTemplateFieldId,
                        principalTable: "RecordTemplateFields",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RecordFieldLinks_RecordTemplateFields_RightFieldId",
                        column: x => x.RightFieldId,
                        principalTable: "RecordTemplateFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecordFieldLinks_RecordTemplateLinks_TemplateLinkId",
                        column: x => x.TemplateLinkId,
                        principalTable: "RecordTemplateLinks",
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

            migrationBuilder.InsertData(
                table: "FieldTypeMappings",
                columns: new[] { "Id", "CreatedAt", "Deleted", "Description", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Text", "Text", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Number", "Number", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Date", "Date", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Email", "Email", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Checkbox", "Checkbox", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Dropdown", "Dropdown", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecordFieldLinks_LeftFieldId",
                table: "RecordFieldLinks",
                column: "LeftFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordFieldLinks_RecordTemplateFieldId",
                table: "RecordFieldLinks",
                column: "RecordTemplateFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordFieldLinks_RightFieldId",
                table: "RecordFieldLinks",
                column: "RightFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordFieldLinks_TemplateLinkId",
                table: "RecordFieldLinks",
                column: "TemplateLinkId");

            migrationBuilder.CreateIndex(
                name: "IX_Records_TemplateId",
                table: "Records",
                column: "TemplateId");

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
                name: "IX_RecordTemplateLinks_LeftTemplateId",
                table: "RecordTemplateLinks",
                column: "LeftTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordTemplateLinks_RightTemplateId",
                table: "RecordTemplateLinks",
                column: "RightTemplateId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChangeLogs");

            migrationBuilder.DropTable(
                name: "FieldTypeMappings");

            migrationBuilder.DropTable(
                name: "RecordFieldLinks");

            migrationBuilder.DropTable(
                name: "RecordTemplateConversions");

            migrationBuilder.DropTable(
                name: "RecordValues");

            migrationBuilder.DropTable(
                name: "RecordTemplateLinks");

            migrationBuilder.DropTable(
                name: "RecordTemplateFields");

            migrationBuilder.DropTable(
                name: "Records");

            migrationBuilder.DropTable(
                name: "RecordTemplateSections");

            migrationBuilder.DropTable(
                name: "RecordTemplates");
        }
    }
}
