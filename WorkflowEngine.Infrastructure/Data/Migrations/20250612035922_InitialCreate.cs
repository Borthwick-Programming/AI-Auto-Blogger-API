using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkflowEngine.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NodeDefinitions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 512, nullable: true),
                    NodeType = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    ConfigurationSchemaJson = table.Column<string>(type: "TEXT", nullable: true),
                    InputsJson = table.Column<string>(type: "TEXT", nullable: false),
                    OutputsJson = table.Column<string>(type: "TEXT", nullable: false),
                    Icon = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    ReactComponent = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeDefinitions", x => x.Id);
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
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Provider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderId = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    OwnerId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NodeInstances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProjectId = table.Column<Guid>(type: "TEXT", nullable: false),
                    NodeTypeId = table.Column<string>(type: "TEXT", nullable: false),
                    ConfigurationJson = table.Column<string>(type: "TEXT", nullable: false),
                    PositionX = table.Column<double>(type: "REAL", nullable: false),
                    PositionY = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NodeInstances_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "NodeConnections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FromNodeInstanceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    FromPortName = table.Column<string>(type: "TEXT", nullable: false),
                    ToNodeInstanceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ToPortName = table.Column<string>(type: "TEXT", nullable: false),
                    ToNodeId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeConnections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NodeConnections_NodeInstances_FromNodeInstanceId",
                        column: x => x.FromNodeInstanceId,
                        principalTable: "NodeInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NodeConnections_NodeInstances_ToNodeId",
                        column: x => x.ToNodeId,
                        principalTable: "NodeInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "AffiliateLinkEntrySchedules",
                columns: table => new
                {
                    EntryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Frequency = table.Column<string>(type: "TEXT", maxLength: 16, nullable: false, defaultValue: "Daily")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AffiliateLinkEntrySchedules", x => x.EntryId);
                    table.ForeignKey(
                        name: "FK_AffiliateLinkEntrySchedules_AffiliateLinkInputEntries_EntryId",
                        column: x => x.EntryId,
                        principalTable: "AffiliateLinkInputEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LinkProcessingHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EntryId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProcessedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkProcessingHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkProcessingHistory_AffiliateLinkInputEntries_EntryId",
                        column: x => x.EntryId,
                        principalTable: "AffiliateLinkInputEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AffiliateLinkInputEntries_NodeInstanceId",
                table: "AffiliateLinkInputEntries",
                column: "NodeInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkProcessingHistory_EntryId",
                table: "LinkProcessingHistory",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_NodeConnections_FromNodeInstanceId",
                table: "NodeConnections",
                column: "FromNodeInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_NodeConnections_ToNodeId",
                table: "NodeConnections",
                column: "ToNodeId");

            migrationBuilder.CreateIndex(
                name: "IX_NodeInstances_ProjectId",
                table: "NodeInstances",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_OwnerId",
                table: "Projects",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AffiliateLinkEntrySchedules");

            migrationBuilder.DropTable(
                name: "LinkProcessingHistory");

            migrationBuilder.DropTable(
                name: "NodeConnections");

            migrationBuilder.DropTable(
                name: "NodeDefinitions");

            migrationBuilder.DropTable(
                name: "PrePrompts");

            migrationBuilder.DropTable(
                name: "AffiliateLinkInputEntries");

            migrationBuilder.DropTable(
                name: "AffiliateLinkInputConfigs");

            migrationBuilder.DropTable(
                name: "NodeInstances");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
