using GoTorz.Model;
using Microsoft.AspNetCore.Mvc;

namespace GoTorz.Components.Controllers
{
    public class TravelBulderController : Controller
    {
        public IActionResult TravelBuilder()
        {
            return View();
        }
        [HttpPost, Route("/create")]
        public IActionResult CreateTravel(string origin, string destination, string departureDate)
        {
            Create
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
