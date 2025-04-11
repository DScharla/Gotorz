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
        public async Task<StayOfferResponse> PostStaysAsync(StayOfferRequest stayOfferRequest)
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

            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync(url, stayOfferRequest, options);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<StayOfferResponse>(responseContent);
            }
            else
            {
                throw new Exception($"API request failed: {httpResponseMessage.StatusCode}");
            }
        }

    }
}
