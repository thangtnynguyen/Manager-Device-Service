using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manager_Device_Service.Domains.Migrations
{
    /// <inheritdoc />
    public partial class update_table_device_log : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DeviceLogs_UserActionId",
                table: "DeviceLogs",
                column: "UserActionId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceLogs_Users_UserActionId",
                table: "DeviceLogs",
                column: "UserActionId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceLogs_Users_UserActionId",
                table: "DeviceLogs");

            migrationBuilder.DropIndex(
                name: "IX_DeviceLogs_UserActionId",
                table: "DeviceLogs");
        }
    }
}
