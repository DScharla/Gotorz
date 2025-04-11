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
<<<<<<< HEAD
        HttpClient _flightHttpClient;
        OfferService? offerService;
        public FlightBuilderController(HttpClient httpClient)
        {
            _flightHttpClient = httpClient;
            offerService = new OfferService(_flightHttpClient);
=======
       
        OfferService? _offerService;
        public FlightBuilderController(OfferService offerService)
        {
           
            _offerService = offerService;
>>>>>>> main
        }

        /*[HttpPost, Route("/create")] //FlightDTO skal passes som argument i JSON format
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

                *//*offerResponses.Add(deparetureTravelResponse);
                offerResponses.Add(returnTravelResponse);*//*
            }
            
            return flightDTOs;

        }
*/
        [HttpPost, Route("create")] //FlightDTO skal passes som argument i JSON format
<<<<<<< HEAD
        public async Task<string> Create(FlightDTO flightDTO)
        {
            FlightDTO departureFlight = flightDTO as FlightDTO;
=======
        public async Task<string> Create([FromBody]FlightDTO flightDTO)
        {
            FlightDTO departureFlight = flightDTO;
>>>>>>> main
            FlightDTO returnFlight = new FlightDTO() 
            { 
                DepartureDate = departureFlight.ReturnDate, 
                Origin = departureFlight.Destination, 
                Destination = departureFlight.Origin 
            };

			List<FlightDTO> flightDTOs = new List<FlightDTO>();
<<<<<<< HEAD
            OfferRequest departureTravel = CreateOfferRequest(departureFlight);
            OfferRequest returnTravel = CreateOfferRequest(returnFlight);

            if (offerService is not null)
            {
                OfferResponse departureTravelResponse = await offerService.PostOfferAsync(departureTravel);
                foreach (var offer in departureTravelResponse.Data.Offers)
                {
                    flightDTOs.Add(new FlightDTO
                    {
                        Origin = offer.FlightsDetail[0].Origin.Name,
                        Destination = offer.FlightsDetail[0].Destination.Name,
                        DepartureDate = DateTime.Parse(offer.FlightsDetail[0].Segments[0].DepartingAt),
                        Price = double.Parse(offer.TotalAmount),
                    });
                }

                OfferResponse returnTravelResponse = await offerService.PostOfferAsync(returnTravel);
=======
            OfferRequest deparetureTravel = CreateOfferRequest(departureFlight);
            OfferRequest returnTravel = CreateOfferRequest(returnFlight);

            if (_offerService is not null)
            {
                OfferResponse deparetureTravelResponse = await _offerService.PostOfferAsync(deparetureTravel);
                    foreach (var offer in deparetureTravelResponse.Data.Offers)
                    {
                        flightDTOs.Add(new FlightDTO
                        {
                            Origin = offer.FlightsDetail[0].Origin.Name,
                            Destination = offer.FlightsDetail[0].Destination.Name,
                            DepartureDate = DateTime.Parse(offer.FlightsDetail[0].Segments[0].DepartingAt),
                            Price = double.Parse(offer.TotalAmount),
                        });
                    }
              
                OfferResponse returnTravelResponse = await _offerService.PostOfferAsync(returnTravel);
>>>>>>> main

                foreach (var offer in returnTravelResponse.Data.Offers)
                {
                    flightDTOs.Add(new FlightDTO
                    {
						Origin = offer.FlightsDetail[0].Origin.Name,
						Destination = offer.FlightsDetail[0].Destination.Name,
						DepartureDate = DateTime.Parse(offer.FlightsDetail[0].Segments[0].DepartingAt),
						Price = double.Parse(offer.TotalAmount),
					});
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
                            DepartureDate = flightDTO.DepartureDate?.ToString("yyyy-MM-dd"),
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
