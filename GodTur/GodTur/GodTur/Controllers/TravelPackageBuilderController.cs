using GodTur.Models;
using GodTur.Models.Context;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace GodTur.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TravelPackageBuilderController : ControllerBase
    {
        private readonly OfferResponseContext _context;
        public TravelPackageBuilderController(OfferResponseContext context)
        {
            _context = context;
        }
        [HttpPost, Route("create")]

        public async Task Create([FromBody] TravelPackageDTO travelPackageDTO)
        {
            TravelPackageDTO travelPackage = travelPackageDTO;
            
            City hotelCity = _context.Cities.Find(travelPackage.PackageHotel.City);

            if (hotelCity != null)
            {
                _context.Hotels.Add(new Hotel
                {
                    Name = travelPackage.PackageHotel.HotelName,
                    CityId = hotelCity.CityId,
                    StayPrice = travelPackage.PackageHotel.Price,
                    CheckInDate = travelPackage.PackageHotel.CheckInDate,
                    CheckOutDate = travelPackage.PackageHotel.CheckOutDate,
                });
                await _context.SaveChangesAsync();
            }
            else
            {

                City newCity = new City
                {
                    Name = travelPackage.PackageHotel.City,
                    IataCountryCode = travelPackage.PackageHotel.Country
                };
                _context.Cities.Add(newCity);

                await _context.SaveChangesAsync();

                _context.Hotels.Add(new Hotel
                {
                    Name = travelPackage.PackageHotel.HotelName,
                    CityId = newCity.CityId,
                    StayPrice = travelPackage.PackageHotel.Price,
                    CheckInDate = travelPackage.PackageHotel.CheckInDate,
                    CheckOutDate = travelPackage.PackageHotel.CheckOutDate,
                });

                await _context.SaveChangesAsync();
            }
            // vi er ved at se på CRUD-funktionaliteten ift. hoteller og flyvninger, pris på flyet giver nogle udfordringer
            //_context.Flights.Add(new Flight
            //{
            //    DepartingAt = travelPackage.OutboundFlight.DepartureDate,
            //    ArrivingAt = travelPackage.ReturnFlight.ReturnDate,
            //}

            //    _context.TravelPackages.Add(travelPackage);

        }
    }
}
