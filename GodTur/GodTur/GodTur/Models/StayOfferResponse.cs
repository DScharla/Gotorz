using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GodTur.Models
{
    public class StayOfferResponse
    {
		[JsonPropertyName("data")]
        public StayData Data { get; set; }
		
    }

    public class StayData
    {
		[JsonPropertyName("created_at")]
        public DateTime Created { get; set; }

		[JsonPropertyName("results")]
		public List<Accommodation>? Accommodations { get; set; }
	}

    public class Accommodation
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("location")]
        public StayLocation Location { get; set; }

        [JsonPropertyName("rooms")]
        public List<Room> Rooms { get; set; }
    }

    public class StayLocation
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
    }

    public class Room
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("rates")]
        public List<Rate> Rates { get; set; }
    }

    public class Rate
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("price")]
        public Price Price { get; set; }

        [JsonPropertyName("cancellation_policy")]
        public CancellationPolicy CancellationPolicy { get; set; }

        [JsonPropertyName("board_type")]
        public string BoardType { get; set; } // fx "breakfast_included"
    }

    public class Price
    {
        [JsonPropertyName("amount")]
        public string Amount { get; set; } // F.eks. "1045.00"

        [JsonPropertyName("currency")]
        public string Currency { get; set; } // F.eks. "DKK"
    }

    public class CancellationPolicy
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } // F.eks. "refundable", "non_refundable"

        [JsonPropertyName("deadline")]
        public DateTime? Deadline { get; set; } // Hvornår det senest kan annulleres
    }
}
