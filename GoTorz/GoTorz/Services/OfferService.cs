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
<<<<<<< HEAD
        public string responseStatus = string.Empty;
=======
>>>>>>> main

        public OfferService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //EN ELLER ANDEN METODE
        public async Task<OfferResponse> PostOfferAsync(OfferRequest offerRequest)
        {
<<<<<<< HEAD
            string url = "/air/offer_requests?return_offers=false&supplier_timeout=10000";
            OfferRequest offerRequest = Test();
=======
            string url = "/air/offer_requests?supplier_timeout=10000";
>>>>>>> main
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                WriteIndented = true
            };
            var responseBody = JsonSerializer.Serialize(offerRequest, options);
<<<<<<< HEAD
            StringContent stringContent = new StringContent(responseBody, Encoding.UTF8,"application/json" );

            var httpResponseMessage = await _httpClient.PostAsync(url, stringContent);
=======
            StringContent stringContent = new StringContent(responseBody, Encoding.UTF8,"application/json");
>>>>>>> main


            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<OfferResponse>(responseContent);
            }
            else
            {
                throw new Exception($"API request failed: {httpResponseMessage.StatusCode}");
            }
<<<<<<< HEAD
            
            
        }

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
=======
        }


        
>>>>>>> main
    }
}
