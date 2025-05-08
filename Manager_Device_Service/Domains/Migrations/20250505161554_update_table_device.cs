using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manager_Device_Service.Domains.Migrations
{
    /// <inheritdoc />
    public partial class update_table_device : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BaseRoomId",
                table: "Devices",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseRoomId",
                table: "Devices");
        }
    }
}
