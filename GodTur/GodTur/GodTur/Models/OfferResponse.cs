using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GodTur.Models
{

    public class OfferResponse
    {
        [JsonPropertyName("data")]
        public DataResponse Data { get; set; }
    }

    public class DataResponse
    {
        [JsonPropertyName("slices")]
        public List<FlightResponse>? Flights { get; set; }

        [JsonPropertyName("offers")]
        public List<Offer>? Offers { get; set; }

        [JsonPropertyName("id")]
        public string? OfferReponseId { get; set; }
    }

    public class FlightResponse
	{
		[JsonPropertyName("origin_type")]
        public string? OriginType { get; set; }

        [JsonPropertyName("origin")]
        public Airport? Origin { get; set; }

        [JsonPropertyName("destination_type")]
        public string? DestinationType { get; set; }

        [JsonPropertyName("destination")]
        public Airport? Destination { get; set; }

        [JsonPropertyName("departure_date")]
        public string? DepartureDate { get; set; }
    }

    public class Airport
    {
		
		[JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("time_zone")]
        public string? TimeZone { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("longitude")]
        public double? Longitude { get; set; }

        [JsonPropertyName("latitude")]
        public double? Latitude { get; set; }

        [JsonPropertyName("id")]
        public string? AirportId { get; set; }

        [JsonPropertyName("icao_code")]
        public string? IcaoCode { get; set; }

        [JsonPropertyName("iata_country_code")]
        public string? IataCountryCode { get; set; }

        [JsonPropertyName("iata_code")]
        public string? IataCode { get; set; }

        [JsonPropertyName("iata_city_code")]
        public string? IataCityCode { get; set; }

        [JsonPropertyName("city_name")]
        public string? CityName { get; set; }

        [JsonPropertyName("city")]
        public City? City { get; set; }

        // Skabte en loop i ER-diagrammet, og var egentlig ikke noget vi fik tilbage i responsen fra duffel.
        //[JsonPropertyName("airports")]
        //public List<Airport>? Airports { get; set; }
    }

    public class City
    {
		[JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("id")]
        public string? CityId { get; set; }

        [JsonPropertyName("iata_country_code")]
        public string? IataCountryCode { get; set; }

        [JsonPropertyName("iata_code")]
        public string? IataCode { get; set; }

        [JsonPropertyName("airports")]
        public List<Airport>? Airports { get; set; }
    }

    public class Offer
    {
        public int? OfferId { get; set; }
        [JsonPropertyName("total_emissions_kg")]
        public string? TotalEmissionsKg { get; set; }

        [JsonPropertyName("total_currency")]
        public string? TotalCurrency { get; set; }

        [JsonPropertyName("total_amount")]
        public string? TotalAmount { get; set; }

        [JsonPropertyName("tax_currency")]
        public string? TaxCurrency { get; set; }

        [JsonPropertyName("tax_amount")]
        public string? TaxAmount { get; set; }

        [JsonPropertyName("supported_passenger_identity_document_types")]
        public List<string>? SupportedPassengerIdentityDocumentTypes { get; set; }

        [JsonPropertyName("slices")]
        public List<FlightDetail>? FlightsDetail { get; set; }
    }

    public class FlightDetail
	{
        public int? FlightId { get; set; }
        [JsonPropertyName("segments")]
        public List<Segment>? Segments { get; set; }

        [JsonPropertyName("origin_type")]
        public string? OriginType { get; set; }

        [JsonPropertyName("origin")]
        public Airport? Origin { get; set; }

        [JsonPropertyName("destination_type")]
        public string? DestinationType { get; set; }

        [JsonPropertyName("destination")]
        public Airport? Destination { get; set; }

        [JsonPropertyName("duration")]
        public string? Duration { get; set; }
    }

    public class Segment
    {
		public int? FlightDetailId { get; set; }
		[JsonPropertyName("stops")]
        public List<Stop>? Stops { get; set; }

        [JsonPropertyName("passengers")]
        public List<PassengerResponse>? Passengers { get; set; }

        [JsonPropertyName("origin_terminal")]
        public string? OriginTerminal { get; set; }

        [JsonPropertyName("origin")]
        public Airport? Origin { get; set; }

        [JsonPropertyName("destination")]
        public Airport? Destination { get; set; }

        [JsonPropertyName("departing_at")]
        public string? DepartingAt { get; set; }

        [JsonPropertyName("arriving_at")]
        public string? ArrivingAt { get; set; }

        [JsonPropertyName("operating_carrier_flight_number")]
        public string? OperatingCarrierFlightNumber { get; set; }

        [JsonPropertyName("marketing_carrier_flight_number")]
        public string? MarketingCarrierFlightNumber { get; set; }
    }

    public class Stop
    {
		[JsonPropertyName("id")]
        public string? StopId { get; set; }

        [JsonPropertyName("duration")]
        public string? Duration { get; set; }

        [JsonPropertyName("departing_at")]
        public string? DepartingAt { get; set; }

        [JsonPropertyName("arriving_at")]
        public string? ArrivingAt { get; set; }

        [JsonPropertyName("airport")]
        public Airport? Airport { get; set; }
    }

    public class PassengerResponse
    {
		[JsonPropertyName("passenger_id")] [Key]
        public string? PassengerId { get; set; }

        [JsonPropertyName("fare_basis_code")]
        public string? FareBasisCode { get; set; }

        [JsonPropertyName("cabin_class_marketing_name")]
        public string? CabinClassMarketingName { get; set; }

        [JsonPropertyName("cabin_class")]
        public string? CabinClass { get; set; }
    }

}