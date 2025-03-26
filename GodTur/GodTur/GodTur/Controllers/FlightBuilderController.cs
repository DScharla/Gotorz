using GodTur.Models;
using GodTur.Services;
using Microsoft.AspNetCore.Mvc;

namespace GodTur.Controllers
{
    public class FlightBuilderController : Controller
    {
        //MANGLER AT BLIVE IMPLEMENTERET: Sortering ift departure time ascending;
        HttpClient _flightHttpClient;
        OfferService? offerService;
        public FlightBuilderController(HttpClient httpClient)
        {
            _flightHttpClient = httpClient;
            offerService = new OfferService(_flightHttpClient);
        }
        public IActionResult TravelBuilder()
        {
            return View();
        }

        [HttpPost, Route("/create")]
        public IActionResult Create(string origin, string destination, string departureDate, string homeDate)
        {
            List<OfferResponse> offerResponses = new List<OfferResponse>();
            OfferRequest deparetureTravel = CreateOfferRequest(origin, destination, departureDate);
            OfferRequest homeTravel = CreateOfferRequest(destination, origin, homeDate);

            if (offerService is not null)
            {
                OfferResponse deparetureTravelResponse = offerService.PostOfferAsync(deparetureTravel).Result;
                OfferResponse homeTravelResponse = offerService.PostOfferAsync(homeTravel).Result;
                offerResponses.Add(deparetureTravelResponse);
                offerResponses.Add(homeTravelResponse);
            }

            return View(offerResponses);

        }
        private OfferRequest CreateOfferRequest(string origin, string destination, string departureDate)
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
                            DepartureDate = departureDate,
                        }
                    },
                    Passengers = new List<PassengerRequest>
                    {
                        new PassengerRequest
                        {
                            Type = "adult",
                        }
                    },
                    MaxConnections = 0,
                }
            };
            return offerRequest;
        }
    }
}
