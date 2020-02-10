using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightBooking.Entities.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airport",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airport", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plane",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ComsumptionEffort = table.Column<double>(nullable: false),
                    ComsumptionRate = table.Column<double>(nullable: false),
                    Speed = table.Column<double>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plane", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flight",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaneId = table.Column<int>(nullable: false),
                    FlightFromId = table.Column<int>(nullable: false),
                    FlightToId = table.Column<int>(nullable: false),
                    FlightComsuption = table.Column<double>(nullable: false),
                    FlightStartTime = table.Column<DateTime>(nullable: false),
                    FlightDuration = table.Column<double>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FlightDistance = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flight", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightFrom_Airport",
                        column: x => x.FlightFromId,
                        principalTable: "Airport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlightTo_Airport",
                        column: x => x.FlightToId,
                        principalTable: "Airport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flight_Plane",
                        column: x => x.PlaneId,
                        principalTable: "Plane",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Airport",
                columns: new[] { "Id", "City", "Country", "CreationDate", "Latitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { 1, "Atlanta, Georgia", "United States", new DateTime(2020, 2, 10, 14, 28, 59, 76, DateTimeKind.Local).AddTicks(6639), -29.832450000000001, 31.04034, "Hartsfield–Jackson Atlanta International Airport" },
                    { 2, "Roissy-en-France, Île-de-Franc", "France", new DateTime(2020, 2, 10, 14, 28, 59, 76, DateTimeKind.Local).AddTicks(8623), -0.83245000000000002, 31.04034, "Paris-Charles de Gaulle Airport" },
                    { 3, "Ōta, Tokyo", "Japan", new DateTime(2020, 2, 10, 14, 28, 59, 76, DateTimeKind.Local).AddTicks(8676), -51.397919999999999, -0.12084, "Tokyo Haneda Airport" },
                    { 4, "Garhoud, Dubai", "United Arab Emirates", new DateTime(2020, 2, 10, 14, 28, 59, 76, DateTimeKind.Local).AddTicks(8680), 77.216700000000003, 28.666699999999999, " Dubai International Airport" },
                    { 5, "Casablanca", "Morocco", new DateTime(2020, 2, 10, 14, 28, 59, 76, DateTimeKind.Local).AddTicks(8685), -34.832450000000001, 28.666699999999999, "Mohammed V Airport" },
                    { 6, "Mississauga, Ontario", "Canada", new DateTime(2020, 2, 10, 14, 28, 59, 76, DateTimeKind.Local).AddTicks(8690), 77.033299999999997, 77.033299999999997, "Toronto Pearson International Airport" },
                    { 7, "Barcelona", "Spain", new DateTime(2020, 2, 10, 14, 28, 59, 76, DateTimeKind.Local).AddTicks(8693), -28.466699999999999, -0.83245000000000002, "Barcelona–El Prat Airport" }
                });

            migrationBuilder.InsertData(
                table: "Plane",
                columns: new[] { "Id", "ComsumptionEffort", "ComsumptionRate", "CreationDate", "Name", "Speed" },
                values: new object[,]
                {
                    { 1, 123.0, 30.0, new DateTime(2020, 2, 10, 14, 28, 59, 72, DateTimeKind.Local).AddTicks(9181), "Wright Flyer", 200.0 },
                    { 2, 340.0, 50.0, new DateTime(2020, 2, 10, 14, 28, 59, 75, DateTimeKind.Local).AddTicks(4805), "Supermarine Spitfire", 600.0 },
                    { 3, 400.0, 100.0, new DateTime(2020, 2, 10, 14, 28, 59, 75, DateTimeKind.Local).AddTicks(4911), "Boeing 787", 1000.0 },
                    { 4, 300.0, 145.0, new DateTime(2020, 2, 10, 14, 28, 59, 75, DateTimeKind.Local).AddTicks(4917), "Learjet 23", 450.0 },
                    { 5, 140.0, 80.0, new DateTime(2020, 2, 10, 14, 28, 59, 75, DateTimeKind.Local).AddTicks(4921), "Lockheed C-130", 500.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flight_FlightFromId",
                table: "Flight",
                column: "FlightFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Flight_FlightToId",
                table: "Flight",
                column: "FlightToId");

            migrationBuilder.CreateIndex(
                name: "IX_Flight_PlaneId",
                table: "Flight",
                column: "PlaneId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flight");

            migrationBuilder.DropTable(
                name: "Airport");

            migrationBuilder.DropTable(
                name: "Plane");
        }
    }
}
