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
        private readonly string _apiKey;
        public string responseStatus = string.Empty;

        public OfferService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //EN ELLER ANDEN METODE
        public async Task<string> PostOfferAsync(/*OfferRequest offerRequest*/)
        {
            string url = "/air/offer_requests?return_offers=false&supplier_timeout=10000";
            OfferRequest offerRequest = Test();
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                WriteIndented = true
            };
            var responseBody = JsonSerializer.Serialize(offerRequest, options);
            StringContent stringContent = new StringContent(responseBody, Encoding.UTF8,"application/json" );

            var httpResponseMessage = await _httpClient.PostAsync(url, stringContent);

            return $"{httpResponseMessage.StatusCode.ToString()}";
            /*if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<OfferResponse>(responseContent);
            }
            else
            {
                throw new Exception($"API request failed: {httpResponseMessage.StatusCode}");
            }*/
            
            
            /*await _httpClient.PostAsJsonAsync<OfferRequest>(url, offerRequest)*/
        }
        /*public static Task<HttpResponseMessage> PostAsJsonAsync<T>(
            this HttpClient client,
            string requestUri,
            T value
        )*/

        public OfferRequest Test()
        {
            var offerRequest = new OfferRequest
            {
                Data = new DataRequest
                {
                    Flight = new List<FlightRequest>
                    {
                        new FlightRequest
                        {
                            Origin = "LHR",
                            Destination = "JFK",
                            DepartureDate = "2025-04-24",
                            DepartureTime = new TimeRange { From = "09:45", To = "17:00" },
                            ArrivalTime = new TimeRange { From = "09:45", To = "17:00" }
                        }
                    },
                    PrivateFares = new Dictionary<string, List<PrivateFare>>
                    {
                        { "QF", new List<PrivateFare> { new PrivateFare { CorporateCode = "FLX53", TrackingReference = "ABN:2345678" } } },
                        { "UA", new List<PrivateFare> { new PrivateFare { CorporateCode = "1234", TourCode = "578DFL" } } }
                    },
                    Passengers = new List<PassengerRequest>
                    {
                        new PassengerRequest
                        {
                            GivenName = "Amelia",
                            FamilyName = "Earhart",
                            Type = "adult",
                            LoyaltyProgrammeAccounts = new List<LoyaltyProgrammeAccount>
                            {
                                new LoyaltyProgrammeAccount { AccountNumber = "12901014", AirlineIataCode = "BA" }
                            }
                        },
                        new PassengerRequest { Age = 14 },
                        new PassengerRequest { FareType = "student" },
                        new PassengerRequest { Age = 5, FareType = "contract_bulk_child" }
                    },
                    MaxConnections = 0,
                    CabinClass = "economy"
                }
            };
            return offerRequest;
        }
    }
}
