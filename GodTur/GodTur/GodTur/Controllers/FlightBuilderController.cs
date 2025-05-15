using GodTur.Models;
using GodTur.Services;
using Microsoft.AspNetCore.Mvc;
using Shared;
using System.Text.Json;

namespace GodTur.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightBuilderController : ControllerBase
    {
        IOfferService? _offerService;
        public FlightBuilderController(IOfferService offerService)
        {
           
            _offerService = offerService;
        }
        [HttpPost, Route("create")]
        public async Task<string> PostCreateAsync([FromBody]FlightDTO flightDTO)
        {
            FlightDTO departureFlight = flightDTO;

            FlightDTO returnFlight = new FlightDTO() 
            { 
                DepartureDate = departureFlight.ReturnDate, 
                Origin = departureFlight.Destination, 
                Destination = departureFlight.Origin 
            };

			List<FlightDTO> flightDTOs = new List<FlightDTO>();
            OfferRequest departureTravel = CreateOfferRequest(departureFlight);
            OfferRequest returnTravel = CreateOfferRequest(returnFlight);
            int i = 0;
            if (_offerService is not null)
            {
                OfferResponse departureTravelResponse = await _offerService.PostOfferAsync(departureTravel);
                foreach (var offer in departureTravelResponse.Data.Offers)
                {
                    flightDTOs.Add(new FlightDTO
                    {
                        ID = i,
                        Origin = offer.FlightsDetail[0].Origin.Name,
                        Destination = offer.FlightsDetail[0].Destination.Name,
                        DepartureDate = DateTime.Parse(offer.FlightsDetail[0].Segments[0].DepartingAt),
                        Price = double.Parse(offer.TotalAmount),
                    });
                    i++;
                }
              
                OfferResponse returnTravelResponse = await _offerService.PostOfferAsync(returnTravel);

                foreach (var offer in returnTravelResponse.Data.Offers)
                {
                    flightDTOs.Add(new FlightDTO
                    {
                        ID = i,
                        Origin = offer.FlightsDetail[0].Origin.Name,
						Destination = offer.FlightsDetail[0].Destination.Name,
						DepartureDate = DateTime.Parse(offer.FlightsDetail[0].Segments[0].DepartingAt),
						Price = double.Parse(offer.TotalAmount),
					});
                    i++;
                }
            }
            flightDTOs.OrderBy(f => f.DepartureDate);
            return JsonSerializer.Serialize(flightDTOs);

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
                            DepartureDate = flightDTO.DepartureDate.ToString("yyyy-MM-dd"),
                        }
                    },
                    Passengers = new List<PassengerRequest>
                    {
                        new PassengerRequest
                        {
                            Type = "adult",
                        }
                    },
                    //MaxConnections = 0,
                }
            };
            return offerRequest;
        }
    }
}
