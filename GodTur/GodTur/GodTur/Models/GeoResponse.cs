using System.Text.Json.Serialization;

namespace GodTur.Models
{
    public class GeoResponse
    {
        [JsonPropertyName("lat")]
        public double Latitude { get; set; }
        [JsonPropertyName("lon")]
        public double Longitude { get; set; }
        
    }
}
