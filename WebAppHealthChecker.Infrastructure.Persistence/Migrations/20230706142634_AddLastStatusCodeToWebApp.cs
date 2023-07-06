using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppHealthChecker.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddLastStatusCodeToWebApp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LastStatusCode",
                schema: "base",
                table: "WebApp",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastStatusCode",
                schema: "base",
                table: "WebApp");
        }
    }
}
