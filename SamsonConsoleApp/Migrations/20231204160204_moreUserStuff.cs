using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SamsonConsoleApp.Migrations
{
    /// <inheritdoc />
    public partial class moreUserStuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "token_type",
                table: "spotifyUserAuths",
                newName: "Token_type");

            migrationBuilder.RenameColumn(
                name: "scope",
                table: "spotifyUserAuths",
                newName: "Scope");

            migrationBuilder.RenameColumn(
                name: "refresh_token",
                table: "spotifyUserAuths",
                newName: "Refresh_token");

            migrationBuilder.RenameColumn(
                name: "expires_in",
                table: "spotifyUserAuths",
                newName: "Expires_in");

            migrationBuilder.RenameColumn(
                name: "expires_at",
                table: "spotifyUserAuths",
                newName: "Expires_at");

            migrationBuilder.RenameColumn(
                name: "access_token",
                table: "spotifyUserAuths",
                newName: "Access_token");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "spotifyUserAuths",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "SamsonUser",
                table: "spotifyUserAuths",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_spotifyUserAuths_samsonUsers_SamsonUser",
                table: "spotifyUserAuths");

            migrationBuilder.DropIndex(
                name: "IX_spotifyUserAuths_SamsonUser",
                table: "spotifyUserAuths");

            migrationBuilder.DropColumn(
                name: "SamsonUser",
                table: "spotifyUserAuths");

            migrationBuilder.RenameColumn(
                name: "Token_type",
                table: "spotifyUserAuths",
                newName: "token_type");

            migrationBuilder.RenameColumn(
                name: "Scope",
                table: "spotifyUserAuths",
                newName: "scope");

            migrationBuilder.RenameColumn(
                name: "Refresh_token",
                table: "spotifyUserAuths",
                newName: "refresh_token");

            migrationBuilder.RenameColumn(
                name: "Expires_in",
                table: "spotifyUserAuths",
                newName: "expires_in");

            migrationBuilder.RenameColumn(
                name: "Expires_at",
                table: "spotifyUserAuths",
                newName: "expires_at");

            migrationBuilder.RenameColumn(
                name: "Access_token",
                table: "spotifyUserAuths",
                newName: "access_token");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "spotifyUserAuths",
                newName: "id");
        }
    }
}
