using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SamsonClient.Migrations
{
    /// <inheritdoc />
    public partial class InitalCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "spotifyUserAuths",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    access_token = table.Column<string>(type: "TEXT", nullable: false),
                    token_type = table.Column<string>(type: "TEXT", nullable: false),
                    scope = table.Column<string>(type: "TEXT", nullable: false),
                    expires_in = table.Column<int>(type: "INTEGER", nullable: false),
                    refresh_token = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spotifyUserAuths", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "spotifyUserAuths");
        }
    }
}
