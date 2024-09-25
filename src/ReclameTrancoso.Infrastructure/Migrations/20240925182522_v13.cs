using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ManagerComplaintComments_Comments_CommentId",
                table: "ManagerComplaintComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ManagerComplaintComments_Complaints_ComplaintId",
                table: "ManagerComplaintComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ManagerComplaintComments_Managers_ManagerId",
                table: "ManagerComplaintComments");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Residents_ResidentId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ResidentId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ResidentId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Residents",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Managers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ManagerId",
                table: "ManagerComplaintComments",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "ComplaintId",
                table: "ManagerComplaintComments",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CommentId",
                table: "ManagerComplaintComments",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_Residents_UserId",
                table: "Residents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_UserId",
                table: "Managers",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ManagerComplaintComments_Comments_CommentId",
                table: "ManagerComplaintComments",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ManagerComplaintComments_Complaints_ComplaintId",
                table: "ManagerComplaintComments",
                column: "ComplaintId",
                principalTable: "Complaints",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ManagerComplaintComments_Managers_ManagerId",
                table: "ManagerComplaintComments",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Users_UserId",
                table: "Managers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Residents_Users_UserId",
                table: "Residents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ManagerComplaintComments_Comments_CommentId",
                table: "ManagerComplaintComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ManagerComplaintComments_Complaints_ComplaintId",
                table: "ManagerComplaintComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ManagerComplaintComments_Managers_ManagerId",
                table: "ManagerComplaintComments");

            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Users_UserId",
                table: "Managers");

            migrationBuilder.DropForeignKey(
                name: "FK_Residents_Users_UserId",
                table: "Residents");

            migrationBuilder.DropIndex(
                name: "IX_Residents_UserId",
                table: "Residents");

            migrationBuilder.DropIndex(
                name: "IX_Managers_UserId",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Managers");

            migrationBuilder.AddColumn<long>(
                name: "ResidentId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ManagerId",
                table: "ManagerComplaintComments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ComplaintId",
                table: "ManagerComplaintComments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CommentId",
                table: "ManagerComplaintComments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ResidentId",
                table: "Users",
                column: "ResidentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ManagerComplaintComments_Comments_CommentId",
                table: "ManagerComplaintComments",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ManagerComplaintComments_Complaints_ComplaintId",
                table: "ManagerComplaintComments",
                column: "ComplaintId",
                principalTable: "Complaints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ManagerComplaintComments_Managers_ManagerId",
                table: "ManagerComplaintComments",
                column: "ManagerId",
                principalTable: "Managers",
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
    }
}
