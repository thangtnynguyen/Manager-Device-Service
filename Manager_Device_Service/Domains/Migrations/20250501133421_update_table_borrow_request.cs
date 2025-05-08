using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manager_Device_Service.Domains.Migrations
{
    /// <inheritdoc />
    public partial class update_table_borrow_request : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Class",
                table: "BorrowRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "BorrowRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BorrowRequests_RoomId",
                table: "BorrowRequests",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowRequests_Rooms_RoomId",
                table: "BorrowRequests",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowRequests_Rooms_RoomId",
                table: "BorrowRequests");

            migrationBuilder.DropIndex(
                name: "IX_BorrowRequests_RoomId",
                table: "BorrowRequests");

            migrationBuilder.DropColumn(
                name: "Class",
                table: "BorrowRequests");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "BorrowRequests");
        }
    }
}
