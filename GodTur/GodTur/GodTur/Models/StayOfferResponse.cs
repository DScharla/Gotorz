using GodTur.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class StayOfferResponse
{
    [JsonPropertyName("data")]
    public StayOfferData? Data { get; set; }
}

public class StayOfferData
{
    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("results")]
    public List<StayOfferResult>? Results { get; set; }
}

public class StayOfferResult
{
    [JsonPropertyName("cheapest_rate_public_currency")]
    public string? CheapestRatePublicCurrency { get; set; }

    [JsonPropertyName("cheapest_rate_public_amount")]
    public string? CheapestRatePublicAmount { get; set; }

    [JsonPropertyName("cheapest_rate_total_amount")]
    public string? CheapestRateTotalAmount { get; set; }

    [JsonPropertyName("cheapest_rate_currency")]
    public string? CheapestRateCurrency { get; set; }

    [JsonPropertyName("guests")]
    public List<StayGuest>? Guests { get; set; }

    [JsonPropertyName("check_out_date")]
    public DateTime? CheckOutDate { get; set; }

    [JsonPropertyName("check_in_date")]
    public DateTime? CheckInDate { get; set; }  

    [JsonPropertyName("rooms")]
    public int? Rooms { get; set; }

    [JsonPropertyName("accommodation")]
    public Accommodation? Accommodation { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }
}

public class StayGuest
{
    [JsonPropertyName("age")]
    public int? Age { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }
}

public class Accommodation
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("payment_instruction_supported")]
    public bool? PaymentInstructionSupported { get; set; }

    [JsonPropertyName("key_collection")]
    public string? KeyCollection { get; set; }

    [JsonPropertyName("check_in_information")]
    public CheckInInformation? CheckInInformation { get; set; }

    [JsonPropertyName("photos")]
    public List<AccommodationPhoto>? Photos { get; set; }
    
    [JsonPropertyName("location")]
    public AccommodationLocation? Location { get; set; }
    
    [JsonPropertyName("amenities")]
    public List<Amenity>? Amenities { get; set; }
    
    [JsonPropertyName("ratings")]
    public List<AccommodationRating>? Ratings { get; set; }

    [JsonPropertyName("review_score")]
    public double? ReviewScore { get; set; }

    [JsonPropertyName("rating")]
    public int? Rating { get; set; }

    [JsonPropertyName("phone_number")]
    public string? PhoneNumber { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }
}

public class CheckInInformation
{
    [JsonPropertyName("check_in_before_time")]
    public string? CheckInBeforeTime { get; set; }
    [JsonPropertyName("check_out_before_time")]
    public string? CheckOutBeforeTime { get; set; }
    [JsonPropertyName("check_in_after_time")]
    public string? CheckInAfterTime { get; set; }
}
    
    public class Amenity
{
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    
    [JsonPropertyName("type")]
    public string? Type { get; set; }
}
public class AccommodationLocation
{
    [JsonPropertyName("address")]
    public Address? Address { get; set; }
    
    [JsonPropertyName("geographic_coordinates")]
    public GeographicalCoordinates? GeograhicCoordinates { get; set; }
}
public class GeographicalCoordinates
{
    [JsonPropertyName("latitude")]
    public double? Latitude { get; set; }
    
    [JsonPropertyName("longitude")]
    public double? Longitude { get; set; }
}
public class Address
{
    [JsonPropertyName("line_one")]
    public string? StreetNameNumber { get; set; }
    
    [JsonPropertyName("city_name")]
    public string? City { get; set; }

    [JsonPropertyName("postal_code")]
    public string? PostalCode { get; set; }
    
    [JsonPropertyName("country_code")]
    public string? CountryCode { get; set; }
    
    [JsonPropertyName("region")]
    public string? Region { get; set; }

}
public class AccommodationRating
{
    [JsonPropertyName("source")]
    public string? Source { get; set; }
    
    [JsonPropertyName("Value")]
    public double? Rating { get; set; }
}
public class AccommodationPhoto
{
    [JsonPropertyName("url")]
    public string? Url { get; set; }
}
