using GoTorz.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace GoTorz.Services
{
    public class OfferService
    {
        private readonly HttpClient _httpClient;
        

        public OfferService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            
        }

        //EN ELLER ANDEN METODE
        public async Task<OfferResponse> PostOfferAsync(string origin, string destination, string departuredate)
        {
            string url = "/air/offer_requests?supplier_timeout=10000";
            OfferRequest offerRequest = CreateOfferRequest(origin, destination, departuredate);
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                WriteIndented = true
            };
            var responseBody = JsonSerializer.Serialize(offerRequest, options);
            StringContent stringContent = new StringContent( responseBody, Encoding.UTF8,"application/json" );

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
            /*await _httpClient.PostAsJsonAsync<OfferRequest>(url, offerRequest)*/
        }
        /*public static Task<HttpResponseMessage> PostAsJsonAsync<T>(
            this HttpClient client,
            string requestUri,
            T value
        )*/

        private OfferRequest CreateOfferRequest(string origin, string destination, string departuredate)
        {
            var offerRequest = new OfferRequest
            {
                Data = new DataRequest
                {
                    Flight = new List<FlightRequest>
        {
            new FlightRequest
            {
                Origin = origin,
                Destination = destination,
                DepartureDate = departuredate,
            }
        },                    
                    Passengers = new List<PassengerRequest>
        {
            new PassengerRequest
            {
                Type = "adult",                
                
            },
        },
                    MaxConnections = 0,
                    
                }
            };
            return offerRequest;
        }
    }
}
