using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GoTorz.Model
{
	public class OfferRequest
	{
		[JsonPropertyName("data")]
		public DataRequest Data { get; set; }
	}

	public class DataRequest
	{
		[JsonPropertyName("slices")]
<<<<<<< HEAD
		public List<FlightRequest>? Flight { get; set; }
=======
		public List<FlightRequest> Flight { get; set; }
>>>>>>> main

		[JsonPropertyName("private_fares")]
		public Dictionary<string, List<PrivateFare>>? PrivateFares { get; set; }

		[JsonPropertyName("passengers")]
		public List<PassengerRequest>? Passengers { get; set; }

		[JsonPropertyName("max_connections")]
		public int? MaxConnections { get; set; }

		[JsonPropertyName("cabin_class")]
		public string? CabinClass { get; set; }
	}

	public class FlightRequest
	{
		[JsonPropertyName("origin")]
		public string? Origin { get; set; }

		[JsonPropertyName("destination")]
		public string? Destination { get; set; }

		[JsonPropertyName("departure_time")]
<<<<<<< HEAD
		public TimeRange? DepartureTime { get; set; }
=======
		public TimeRange DepartureTime { get; set; }
>>>>>>> main

		[JsonPropertyName("departure_date")]
		public string? DepartureDate { get; set; }

		[JsonPropertyName("arrival_time")]
		public TimeRange? ArrivalTime { get; set; }
	}

	public class TimeRange
	{
		[JsonPropertyName("from")]
		public string? From { get; set; }

		[JsonPropertyName("to")]
		public string? To { get; set; }
	}

	public class PrivateFare
	{
		[JsonPropertyName("corporate_code")]
		public string? CorporateCode { get; set; }

		[JsonPropertyName("tracking_reference")]
		public string? TrackingReference { get; set; }

		[JsonPropertyName("tour_code")]
		public string? TourCode { get; set; }
	}

	public class PassengerRequest
	{
		[JsonPropertyName("family_name")]
		public string? FamilyName { get; set; }

		[JsonPropertyName("given_name")]
		public string? GivenName { get; set; }

		[JsonPropertyName("loyalty_programme_accounts")]
		public List<LoyaltyProgrammeAccount>? LoyaltyProgrammeAccounts { get; set; }

		[JsonPropertyName("type")]
		public string? Type { get; set; }

		[JsonPropertyName("age")]
		public int? Age { get; set; }

		[JsonPropertyName("fare_type")]
		public string? FareType { get; set; }
	}

	public class LoyaltyProgrammeAccount
	{
		[JsonPropertyName("account_number")]
		public string? AccountNumber { get; set; }

		[JsonPropertyName("airline_iata_code")]
		public string? AirlineIataCode { get; set; }
	}
}
