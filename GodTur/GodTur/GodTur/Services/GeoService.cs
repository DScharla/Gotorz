using GodTur.Models;
using Shared;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace GodTur.Services
{
    public class GeoService : IGeoService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        public async Task<GeoResponse> GetGeographicCoordinatesAsync(StayDTO stayDTO)
        {
            string url = "https://nominatim.openstreetmap.org/search?";
            string query = $"q={stayDTO.StayAdress.City},{stayDTO.StayAdress.Country}";
            string format = "&limit=1&format=json";

            string requestUrl = url + query + format;
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(requestUrl);
            if(httpResponseMessage.IsSuccessStatusCode)
            {
                if (httpResponseMessage.Content != null)
                {
                    string responseContent = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    return JsonSerializer.Deserialize<GeoResponse>(responseContent);                    
                }
                else
                {
                    throw new Exception("No content in response");
                }
            }
            else
            {
                throw new Exception($"API request failed: {httpResponseMessage.StatusCode}");
            }
        }
    }
}
