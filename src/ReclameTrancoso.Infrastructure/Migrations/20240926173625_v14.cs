using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ResidentId",
                table: "Complaints",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_ResidentId",
                table: "Complaints",
                column: "ResidentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Residents_ResidentId",
                table: "Complaints",
                column: "ResidentId",
                principalTable: "Residents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Residents_ResidentId",
                table: "Complaints");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_ResidentId",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "ResidentId",
                table: "Complaints");
        }
    }
}
