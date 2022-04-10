using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Signal.Server.Migrations
{
    public partial class SensorStuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthorizedSensors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MAC = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApiKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorizedSensors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SensorStatusTracks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SensorId = table.Column<int>(type: "int", nullable: false),
                    CreatedTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorStatusTracks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SensorStatusTracks_AuthorizedSensors_SensorId",
                        column: x => x.SensorId,
                        principalTable: "AuthorizedSensors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorizedSensors_MAC_ApiKey",
                table: "AuthorizedSensors",
                columns: new[] { "MAC", "ApiKey" });

            migrationBuilder.CreateIndex(
                name: "IX_SensorStatusTracks_SensorId",
                table: "SensorStatusTracks",
                column: "SensorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SensorStatusTracks");

            migrationBuilder.DropTable(
                name: "AuthorizedSensors");
        }
    }
}
