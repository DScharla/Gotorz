using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GodTur.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IataCountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IataCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "FlightOffers",
                columns: table => new
                {
                    OfferId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalEmissionsKg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalCurrency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxCurrency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupportedPassengerIdentityDocumentTypes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightOffers", x => x.OfferId);
                });

            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    AirportId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeZone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    IcaoCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IataCountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IataCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IataCityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AirportId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.AirportId);
                    table.ForeignKey(
                        name: "FK_Airports_Airports_AirportId1",
                        column: x => x.AirportId1,
                        principalTable: "Airports",
                        principalColumn: "AirportId");
                    table.ForeignKey(
                        name: "FK_Airports_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId");
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    FlightDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginAirportId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DestinationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DestinationAirportId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfferId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.FlightDetailId);
                    table.ForeignKey(
                        name: "FK_Flights_Airports_DestinationAirportId",
                        column: x => x.DestinationAirportId,
                        principalTable: "Airports",
                        principalColumn: "AirportId");
                    table.ForeignKey(
                        name: "FK_Flights_Airports_OriginAirportId",
                        column: x => x.OriginAirportId,
                        principalTable: "Airports",
                        principalColumn: "AirportId");
                    table.ForeignKey(
                        name: "FK_Flights_FlightOffers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "FlightOffers",
                        principalColumn: "OfferId");
                });

            migrationBuilder.CreateTable(
                name: "Segments",
                columns: table => new
                {
                    SegmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginTerminal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginAirportId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DestinationAirportId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DepartingAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArrivingAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperatingCarrierFlightNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarketingCarrierFlightNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlightDetailId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Segments", x => x.SegmentId);
                    table.ForeignKey(
                        name: "FK_Segments_Airports_DestinationAirportId",
                        column: x => x.DestinationAirportId,
                        principalTable: "Airports",
                        principalColumn: "AirportId");
                    table.ForeignKey(
                        name: "FK_Segments_Airports_OriginAirportId",
                        column: x => x.OriginAirportId,
                        principalTable: "Airports",
                        principalColumn: "AirportId");
                    table.ForeignKey(
                        name: "FK_Segments_Flights_FlightDetailId",
                        column: x => x.FlightDetailId,
                        principalTable: "Flights",
                        principalColumn: "FlightDetailId");
                });

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    PassengerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FareBasisCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CabinClassMarketingName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CabinClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SegmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.PassengerId);
                    table.ForeignKey(
                        name: "FK_Passengers_Segments_SegmentId",
                        column: x => x.SegmentId,
                        principalTable: "Segments",
                        principalColumn: "SegmentId");
                });

            migrationBuilder.CreateTable(
                name: "Stops",
                columns: table => new
                {
                    StopId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartingAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArrivingAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirportId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SegmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stops", x => x.StopId);
                    table.ForeignKey(
                        name: "FK_Stops_Airports_AirportId",
                        column: x => x.AirportId,
                        principalTable: "Airports",
                        principalColumn: "AirportId");
                    table.ForeignKey(
                        name: "FK_Stops_Segments_SegmentId",
                        column: x => x.SegmentId,
                        principalTable: "Segments",
                        principalColumn: "SegmentId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Airports_AirportId1",
                table: "Airports",
                column: "AirportId1");

            migrationBuilder.CreateIndex(
                name: "IX_Airports_CityId",
                table: "Airports",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_DestinationAirportId",
                table: "Flights",
                column: "DestinationAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_OfferId",
                table: "Flights",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_OriginAirportId",
                table: "Flights",
                column: "OriginAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_SegmentId",
                table: "Passengers",
                column: "SegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Segments_DestinationAirportId",
                table: "Segments",
                column: "DestinationAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_Segments_FlightDetailId",
                table: "Segments",
                column: "FlightDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Segments_OriginAirportId",
                table: "Segments",
                column: "OriginAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_Stops_AirportId",
                table: "Stops",
                column: "AirportId");

            migrationBuilder.CreateIndex(
                name: "IX_Stops_SegmentId",
                table: "Stops",
                column: "SegmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "Stops");

            migrationBuilder.DropTable(
                name: "Segments");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Airports");

            migrationBuilder.DropTable(
                name: "FlightOffers");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
