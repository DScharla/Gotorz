using GodTur.Models;
using GodTur.Services;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace GodTur.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpPost, Route("/create")] //FlightDTO skal passes som argument i JSON format
        public async Task<List<FlightDTO>> Create(FlightDTO departureFlightDTO, FlightDTO returnFlightDTO)
        {
            List<FlightDTO> flightDTOs = new List<FlightDTO>();
            OfferRequest deparetureTravel = CreateOfferRequest(departureFlightDTO);
            OfferRequest returnTravel = CreateOfferRequest(returnFlightDTO);

            if (offerService is not null)
            {
                OfferResponse deparetureTravelResponse = await offerService.PostOfferAsync(deparetureTravel);

                foreach (var flight in deparetureTravelResponse.Data.Flights)
                {
                    flightDTOs.Add(new FlightDTO
                    {
                        Origin = flight.Origin.Name,
                        Destination = flight.Destination.Name,
                        DepartureDate = DateTime.Parse(flight.DepartureDate),
                    });
                }
                
                OfferResponse returnTravelResponse = await offerService.PostOfferAsync(returnTravel);

                foreach (var flight in returnTravelResponse.Data.Flights)
                {
                    flightDTOs.Add(new FlightDTO
                    {
                        Origin = flight.Origin.Name,
                        Destination = flight.Destination.Name,
                        DepartureDate = DateTime.Parse(flight.DepartureDate),
                    });
                }

                /*offerResponses.Add(deparetureTravelResponse);
                offerResponses.Add(returnTravelResponse);*/
            }
            
            return flightDTOs;

        }
        private OfferRequest CreateOfferRequest(FlightDTO flightDTO)
        {
            var offerRequest = new OfferRequest
            {
                Data = new DataRequest
                {
                    Flight = new List<FlightRequest>
                    {
                        new FlightRequest
                        {
                            Origin = flightDTO.Origin,
                            Destination = flightDTO.Destination,
                            DepartureDate = flightDTO.DepartureDate.Date.ToString(),
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
