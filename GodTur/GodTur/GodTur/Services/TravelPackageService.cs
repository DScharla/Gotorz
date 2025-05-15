using GodTur.Models;
using GodTur.Models.Context;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace GodTur.Services
{
	public class TravelPackageService
	{
		private readonly OfferResponseContext _context;
		public TravelPackageService(OfferResponseContext context) 
		{ 
			_context = context;
		}
		public async Task<TravelPackage?> CreateTravelPackageAsync(TravelPackageDTO dto)
		{
			// 1. Lookup or create City
			var city = await _context.Cities
				.Include(c => c.Country)
				.FirstOrDefaultAsync(c => c.Name == dto.PackageHotel.City && c.Country.Name == dto.PackageHotel.Country);

			if (city == null)
			{
				var country = await _context.Countries.FirstOrDefaultAsync(c => c.Name == dto.PackageHotel.Country);
				if (country == null) return null;

				city = new City
				{
					Name = dto.PackageHotel.City,
					CountryId = country.CountryId
				};
				_context.Cities.Add(city);
				await _context.SaveChangesAsync();
			}

			// 2. Lookup or create Hotel
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
					CityId = city.CityId
				};
				_context.Hotels.Add(hotel);
				await _context.SaveChangesAsync();
			}

			// 3. Lookup Airports
			var originAirport = await _context.Airports.FirstOrDefaultAsync(a => a.IataCode == dto.OutboundFlight.Origin);
			var destinationAirport = await _context.Airports.FirstOrDefaultAsync(a => a.IataCode == dto.OutboundFlight.Destination);
			if (originAirport == null || destinationAirport == null) return null;

			// 4. Create Outbound and Return Flights
			var outboundFlight = new Flight
			{
				OriginAirportId = originAirport.AirportId,
				DestinationAirportId = destinationAirport.AirportId,
				DepartingAt = dto.OutboundFlight.DepartureDate ?? DateTime.MinValue,
				ArrivingAt = dto.OutboundFlight.ReturnDate ?? DateTime.MinValue,
				FlightPrice = (decimal)(dto.OutboundFlight.Price ?? 0),
				FlightNumber = dto.OutboundFlight.FlightNumber,
				FPCurrency = "DKK" // Default currency for now
			};

			var returnFlight = new Flight
			{
				OriginAirportId = destinationAirport.AirportId,
				DestinationAirportId = originAirport.AirportId,
				DepartingAt = dto.ReturnFlight.DepartureDate ?? DateTime.MinValue,
				ArrivingAt = dto.ReturnFlight.ReturnDate ?? DateTime.MinValue,
				FlightPrice = (decimal)(dto.ReturnFlight.Price ?? 0),
				FlightNumber = dto.ReturnFlight.FlightNumber,
				FPCurrency = "DKK"
			};

			_context.Flights.AddRange(outboundFlight, returnFlight);
			await _context.SaveChangesAsync();

			// 5. Create TravelPackage
			var package = new TravelPackage
			{
				OutboundFlightId = outboundFlight.FlightId,
				ReturnFlightId = returnFlight.FlightId,
				PackageHotelId = hotel.HotelId,
				PackagePrice = ((decimal)(dto.OutboundFlight.Price ?? 0) + (decimal)(dto.ReturnFlight.Price ?? 0) + (dto.PackageHotel.Price ?? 0))
			};

			_context.TravelPackages.Add(package);
			await _context.SaveChangesAsync();

			return package;
		}


	}
}
