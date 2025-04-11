using GodTur.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GodTur.Services
{
    public class StaysService : IStaysService
    {
        private readonly HttpClient _httpClient;
        public StaysService(DuffelClient duffelClient)
        {
            _httpClient = duffelClient.HttpClient;
        }
        public async Task<AcomResponse> PostStaysAsync(StaysRequest stayRequest)
        {
            string url = "stays/search";
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                WriteIndented = true
            };
/*            var responseBody = JsonSerializer.Serialize(acomRequest, options);
            StringContent stringContent = new StringContent(responseBody, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsync(url, stringContent);*/

            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync(url, stayRequest, options);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<AcomResponse>(responseContent);
            }
            else
            {
                throw new Exception($"API request failed: {httpResponseMessage.StatusCode}");
            }
        }

    }
    public class StaysRequest
    {
        public string CheckIn { get; set; } = "2025-06-01";
        public string CheckOut { get; set; } = "2025-06-06";
        public int Guests { get; set; } = 1;
        public int Rooms { get; set; } = 1;
    }

    public class AcomResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
