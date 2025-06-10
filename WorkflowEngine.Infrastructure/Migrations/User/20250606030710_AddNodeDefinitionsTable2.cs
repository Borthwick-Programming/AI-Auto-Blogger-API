using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkflowEngine.Infrastructure.Migrations.User
{
    /// <inheritdoc />
    public partial class AddNodeDefinitionsTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NodeConnections_NodeInstances_ToNodeInstanceId",
                table: "NodeConnections");

            migrationBuilder.DropIndex(
                name: "IX_NodeConnections_ToNodeInstanceId",
                table: "NodeConnections");

            migrationBuilder.AddColumn<Guid>(
                name: "ToNodeId",
                table: "NodeConnections",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.CreateIndex(
                name: "IX_NodeConnections_ToNodeId",
                table: "NodeConnections",
                column: "ToNodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_NodeConnections_NodeInstances_ToNodeId",
                table: "NodeConnections",
                column: "ToNodeId",
                principalTable: "NodeInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NodeConnections_NodeInstances_ToNodeId",
                table: "NodeConnections");

            migrationBuilder.DropTable(
                name: "NodeDefinitions");

            migrationBuilder.DropIndex(
                name: "IX_NodeConnections_ToNodeId",
                table: "NodeConnections");

            migrationBuilder.DropColumn(
                name: "ToNodeId",
                table: "NodeConnections");

            migrationBuilder.CreateIndex(
                name: "IX_NodeConnections_ToNodeInstanceId",
                table: "NodeConnections",
                column: "ToNodeInstanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_NodeConnections_NodeInstances_ToNodeInstanceId",
                table: "NodeConnections",
                column: "ToNodeInstanceId",
                principalTable: "NodeInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
