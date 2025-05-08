using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manager_Device_Service.Domains.Migrations
{
    /// <inheritdoc />
    public partial class update_table_borrow_request_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BorrowRequests_UserActionId",
                table: "BorrowRequests",
                column: "UserActionId");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowRequests_Users_UserActionId",
                table: "BorrowRequests",
                column: "UserActionId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowRequests_Users_UserActionId",
                table: "BorrowRequests");

            migrationBuilder.DropIndex(
                name: "IX_BorrowRequests_UserActionId",
                table: "BorrowRequests");
        }
    }
}
