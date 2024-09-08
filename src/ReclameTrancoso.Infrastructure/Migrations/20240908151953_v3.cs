using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TokenEntities_Users_UserId",
                table: "TokenEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Residents_ResidentId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TokenId",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_TokenEntities_Users_UserId",
                table: "TokenEntities",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Residents_ResidentId",
                table: "Users",
                column: "ResidentId",
                principalTable: "Residents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TokenEntities_Users_UserId",
                table: "TokenEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Residents_ResidentId",
                table: "Users");

            migrationBuilder.AddColumn<long>(
                name: "TokenId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TokenEntities_Users_UserId",
                table: "TokenEntities",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Residents_ResidentId",
                table: "Users",
                column: "ResidentId",
                principalTable: "Residents",
                principalColumn: "Id");
        }
    }
}
