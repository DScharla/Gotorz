using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GodTur.Models
{
    public class StayOfferRequest
    {
        [JsonPropertyName("data")]
        public StayDataRequest Data { get; set; }
    }

    public class StayDataRequest
    {
        [JsonPropertyName("location")]
        public Location Location { get; set; }

        [JsonPropertyName("check_in_date")]
        public string CheckInDate { get; set; } // Format: "YYYY-MM-DD"

        [JsonPropertyName("check_out_date")]
        public string CheckOutDate { get; set; } // Format: "YYYY-MM-DD"

        [JsonPropertyName("guests")]
        public List<Guest> Guests { get; set; }

        [JsonPropertyName("rooms")]
        public int Rooms { get; set; }
    }

    public class Location
    {
        [JsonPropertyName("radius")]
        public int Radius { get; set; } // Radius i kilometer

        [JsonPropertyName("geographic_coordinates")]
        public GeographicCoordinates GeographicCoordinates { get; set; }
    }

    public class GeographicCoordinates
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
    }

    public class Guest
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } // "adult" eller "child"

        [JsonPropertyName("age")]
        public int? Age { get; set; } // Påkrævet, hvis type er "child"
    }
}
