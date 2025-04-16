using System.Text.Json.Serialization;

namespace GodTur.Models
{
    public class GeoResponse
    {
        [JsonPropertyName("lat")]
        public string Latitude { get; set; }
        [JsonPropertyName("lon")]
        public string Longitude { get; set; }
        
    }
}
