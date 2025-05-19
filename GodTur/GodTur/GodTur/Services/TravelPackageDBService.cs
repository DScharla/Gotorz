using GodTur.Models;
using GodTur.Models.Context;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace GodTur.Services
{
	public class TravelPackageDBService
	{
		private readonly TravelPackageContext _context;
		public TravelPackageDBService(TravelPackageContext context)
		{
			_context = context;
		}
		public async Task<TravelPackage> CreateTravelPackageAsync(TravelPackageDTO dto)
		{
			// 1. Try to find the City with its Country
			var city = await _context.Cities
				.Include(c => c.Country)
				.FirstOrDefaultAsync(c => c.Name == dto.PackageHotel.City && c.Country.Name == dto.PackageHotel.Country);

			// If city not found, try to find or reject the Country
			if (city == null)
			{
				var country = await _context.Countries.FirstOrDefaultAsync(c => c.Name == dto.PackageHotel.Country);

				city = new City
				{
					Name = dto.PackageHotel.City,
					CountryId = country.CountryId
				};
				_context.Cities.Add(city);
				// Don't call SaveChanges yet
			}

			// 2. Try to find the Hotel
			var hotel = await _context.Hotels.FirstOrDefaultAsync(h =>
				h.Name == dto.PackageHotel.HotelName &&
				h.CheckInDate == dto.PackageHotel.CheckInDate &&
				h.CheckOutDate == dto.PackageHotel.CheckOutDate &&
				h.CityId == city.CityId);

			if (hotel == null)
			{
				hotel = new Hotel
				{
					Name = dto.PackageHotel.HotelName,
					CheckInDate = dto.PackageHotel.CheckInDate,
					CheckOutDate = dto.PackageHotel.CheckOutDate,
					StayPrice = dto.PackageHotel.Price,
					City = city
				};
				_context.Hotels.Add(hotel);
			}

			// 3. Lookup airports
			var originAirport = await _context.Airports.FirstOrDefaultAsync(a => a.IataCode == dto.OutboundFlight.OriginIata);
			var destinationAirport = await _context.Airports.FirstOrDefaultAsync(a => a.IataCode == dto.OutboundFlight.DestinationIata);

			// 4. Create outbound + return flights
			var outboundFlight = new Flight
			{
				OriginAirportId = destinationAirport.AirportId,
				DestinationAirportId = originAirport.AirportId,
				DepartingAt = dto.OutboundFlight.DepartureDate,
				ArrivingAt = dto.OutboundFlight.ReturnDate,
				FlightPrice = dto.OutboundFlight.Price ?? 0,
				FlightNumber = dto.OutboundFlight.FlightNumber,
				FPCurrency = "DKK"
			};

			var returnFlight = new Flight
			{
				OriginAirportId = destinationAirport.AirportId,
				DestinationAirportId = originAirport.AirportId,
				DepartingAt = dto.ReturnFlight.DepartureDate,
				ArrivingAt = dto.ReturnFlight.ReturnDate,
				FlightPrice = dto.ReturnFlight.Price ?? 0,
				FlightNumber = dto.ReturnFlight.FlightNumber,
				FPCurrency = "DKK"
			};

			_context.Flights.AddRange(outboundFlight, returnFlight);

			// 5. Save changes so we get all IDs
			await _context.SaveChangesAsync();

			// 6. Create TravelPackage using tracked entities
			var package = new TravelPackage
			{
				OutboundFlightId = outboundFlight.FlightId,
				ReturnFlightId = returnFlight.FlightId,
				PackageHotelId = hotel.HotelId,
				PackagePrice = (dto.OutboundFlight.Price ?? 0) + (dto.ReturnFlight.Price ?? 0) + (dto.PackageHotel.Price ?? 0)
			};

			_context.TravelPackages.Add(package);
			await _context.SaveChangesAsync();

			return package;
		}

	}
}
