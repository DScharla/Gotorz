using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GodTur.Services
{
    public class AcomService
    {
        private readonly HttpClient _httpClient;
        public AcomService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<AcomResponse> PostAcomAsync(AcomRequest acomRequest)
        {
            string url = "accommodation/offer_requests?supplier_timeout=10000";
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                WriteIndented = true
            };
            var responseBody = JsonSerializer.Serialize(acomRequest, options);
            StringContent stringContent = new StringContent(responseBody, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsync(url, stringContent);
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
}
