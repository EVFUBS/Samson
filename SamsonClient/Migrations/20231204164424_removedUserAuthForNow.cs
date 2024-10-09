using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SamsonClient.Migrations
{
    /// <inheritdoc />
    public partial class removedUserAuthForNow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_spotifyUserAuths_samsonUsers_SamsonUser",
                table: "spotifyUserAuths");

            migrationBuilder.DropTable(
                name: "samsonUsers");

            migrationBuilder.DropIndex(
                name: "IX_spotifyUserAuths_SamsonUser",
                table: "spotifyUserAuths");

            migrationBuilder.DropColumn(
                name: "SamsonUser",
                table: "spotifyUserAuths");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SamsonUser",
                table: "spotifyUserAuths",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "samsonUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_samsonUsers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_spotifyUserAuths_SamsonUser",
                table: "spotifyUserAuths",
                column: "SamsonUser");

            migrationBuilder.AddForeignKey(
                name: "FK_spotifyUserAuths_samsonUsers_SamsonUser",
                table: "spotifyUserAuths",
                column: "SamsonUser",
                principalTable: "samsonUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
