using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UnionComplaintComments_ComplaintId",
                table: "UnionComplaintComments");

            migrationBuilder.CreateIndex(
                name: "IX_UnionComplaintComments_ComplaintId",
                table: "UnionComplaintComments",
                column: "ComplaintId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UnionComplaintComments_ComplaintId",
                table: "UnionComplaintComments");

            migrationBuilder.CreateIndex(
                name: "IX_UnionComplaintComments_ComplaintId",
                table: "UnionComplaintComments",
                column: "ComplaintId");
        }
    }
}
