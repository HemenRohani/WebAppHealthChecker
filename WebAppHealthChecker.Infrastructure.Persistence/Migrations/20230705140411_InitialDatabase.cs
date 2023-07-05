using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppHealthChecker.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "base");

            migrationBuilder.CreateTable(
                name: "User",
                schema: "base",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebApp",
                schema: "base",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CheckInterval = table.Column<int>(type: "int", nullable: false),
                    LastCheck = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebApp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebApp_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "base",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebAppCheck",
                schema: "base",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WebAppId = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusCode = table.Column<int>(type: "int", nullable: false),
                    WebAppCheckId = table.Column<int>(type: "int", nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebAppCheck", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebAppCheck_WebAppCheck_WebAppCheckId",
                        column: x => x.WebAppCheckId,
                        principalSchema: "base",
                        principalTable: "WebAppCheck",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WebAppCheck_WebApp_WebAppId",
                        column: x => x.WebAppId,
                        principalSchema: "base",
                        principalTable: "WebApp",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WebApp_UserId",
                schema: "base",
                table: "WebApp",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WebAppCheck_WebAppCheckId",
                schema: "base",
                table: "WebAppCheck",
                column: "WebAppCheckId");

            migrationBuilder.CreateIndex(
                name: "IX_WebAppCheck_WebAppId",
                schema: "base",
                table: "WebAppCheck",
                column: "WebAppId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebAppCheck",
                schema: "base");

            migrationBuilder.DropTable(
                name: "WebApp",
                schema: "base");

            migrationBuilder.DropTable(
                name: "User",
                schema: "base");
        }
    }
}
