using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkflowEngine.Infrastructure.Migrations.User
{
    /// <inheritdoc />
    public partial class AddAffiliateLinkConfigAndPrePrompt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AffiliateLinkInputConfigs",
                columns: table => new
                {
                    NodeInstanceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Mode = table.Column<string>(type: "TEXT", maxLength: 16, nullable: false),
                    CsvPath = table.Column<string>(type: "TEXT", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AffiliateLinkInputConfigs", x => x.NodeInstanceId);
                    table.ForeignKey(
                        name: "FK_AffiliateLinkInputConfigs_NodeInstances_NodeInstanceId",
                        column: x => x.NodeInstanceId,
                        principalTable: "NodeInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrePrompts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    PromptText = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrePrompts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AffiliateLinkInputEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NodeInstanceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Url = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: true),
                    PrePromptName = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AffiliateLinkInputEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AffiliateLinkInputEntries_AffiliateLinkInputConfigs_NodeInstanceId",
                        column: x => x.NodeInstanceId,
                        principalTable: "AffiliateLinkInputConfigs",
                        principalColumn: "NodeInstanceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AffiliateLinkInputEntries_NodeInstanceId",
                table: "AffiliateLinkInputEntries",
                column: "NodeInstanceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AffiliateLinkInputEntries");

            migrationBuilder.DropTable(
                name: "PrePrompts");

            migrationBuilder.DropTable(
                name: "AffiliateLinkInputConfigs");
        }
    }
}
