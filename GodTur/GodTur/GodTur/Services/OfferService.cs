using GodTur.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace GodTur.Services
{
    public class OfferService : IOfferService
    {
        private readonly HttpClient _httpClient;

        public OfferService(DuffelClient duffelClient)
        {
            _httpClient = duffelClient.HttpClient;
        }

        //EN ELLER ANDEN METODE
        public async Task<OfferResponse> PostOfferAsync(OfferRequest offerRequest)
        {
            string url = "air/offer_requests?supplier_timeout=10000";
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                WriteIndented = true
            };
            var responseBody = JsonSerializer.Serialize(offerRequest, options);
            StringContent stringContent = new StringContent(responseBody, Encoding.UTF8,"application/json");

            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsync(url, stringContent);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<OfferResponse>(responseContent);
            }
            else
            {
                throw new Exception($"API request failed: {httpResponseMessage.StatusCode}");
            }
        }


        
    }
}
