using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Signal.Server.Migrations
{
    public partial class SensorMACUniqueness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AuthorizedSensors_MAC",
                table: "AuthorizedSensors",
                column: "MAC",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AuthorizedSensors_MAC",
                table: "AuthorizedSensors");
        }
    }
}
