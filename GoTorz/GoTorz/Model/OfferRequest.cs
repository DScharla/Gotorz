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
		[JsonPropertyName("flight")]
		public List<FlightRequest> Flight { get; set; }

		[JsonPropertyName("privateFares")]
		public Dictionary<string, List<PrivateFare>> PrivateFares { get; set; }

		[JsonPropertyName("passengers")]
		public List<PassengerRequest> Passengers { get; set; }

		[JsonPropertyName("maxConnections")]
		public int MaxConnections { get; set; }

		[JsonPropertyName("cabinClass")]
		public string CabinClass { get; set; }
	}

	public class FlightRequest
	{
		[JsonPropertyName("origin")]
		public string Origin { get; set; }

		[JsonPropertyName("destination")]
		public string Destination { get; set; }

		[JsonPropertyName("departureTime")]
		public TimeRange DepartureTime { get; set; }

		[JsonPropertyName("departureDate")]
		public string DepartureDate { get; set; }

		[JsonPropertyName("arrivalTime")]
		public TimeRange ArrivalTime { get; set; }
	}

	public class TimeRange
	{
		[JsonPropertyName("from")]
		public string From { get; set; }

		[JsonPropertyName("to")]
		public string To { get; set; }
	}

	public class PrivateFare
	{
		[JsonPropertyName("corporateCode")]
		public string CorporateCode { get; set; }

		[JsonPropertyName("trackingReference")]
		public string TrackingReference { get; set; }

		[JsonPropertyName("tourCode")]
		public string TourCode { get; set; }
	}

	public class PassengerRequest
	{
		[JsonPropertyName("familyName")]
		public string FamilyName { get; set; }

		[JsonPropertyName("givenName")]
		public string GivenName { get; set; }

		[JsonPropertyName("loyaltyProgrammeAccounts")]
		public List<LoyaltyProgrammeAccount> LoyaltyProgrammeAccounts { get; set; }

		[JsonPropertyName("type")]
		public string Type { get; set; }

		[JsonPropertyName("age")]
		public int? Age { get; set; }

		[JsonPropertyName("fareType")]
		public string FareType { get; set; }
	}

	public class LoyaltyProgrammeAccount
	{
		[JsonPropertyName("accountNumber")]
		public string AccountNumber { get; set; }

		[JsonPropertyName("airlineIataCode")]
		public string AirlineIataCode { get; set; }
	}
}
