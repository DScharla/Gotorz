using System.Globalization;
using System.Text.Json;
using GodTur.Models;
using GodTur.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace GodTur.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class HotelBuilderController : ControllerBase
	{
		IStaysService? _staysService;
		IGeoService? _geoService;
        public HotelBuilderController(IStaysService staysService, IGeoService geoService)
		{
            _geoService = geoService;
            _staysService = staysService;
		}

		[HttpPost, Route("stays")]
		public async Task<string> Stays([FromBody] StayDTO stayDTO)
		{
			StayDTO stayParam = stayDTO;
            int i = 0;

            List<StayDTO> stayDTOs = new List<StayDTO>();
			StayOfferRequest stayOfferRequest = await CreateStayOfferRequest(stayParam);

			if (_staysService is not null)
			{

				StayOfferResponse stayOfferResponse = await _staysService.PostStaysAsync(stayOfferRequest);
				foreach (var hotel in stayOfferResponse.Data.Results)
				{
					stayDTOs.Add(new StayDTO
					{
                        ID = i,
                        HotelName = hotel.Accommodation.Name,
                        Price = Decimal.Parse(hotel.CheapestRateTotalAmount, CultureInfo.InvariantCulture),
						Currency = hotel.CheapestRateCurrency,
                        City = hotel.Accommodation.Location.Address.City,
                        Country = stayDTO.Country,
                        StreetNameNumber = hotel.Accommodation.Location.Address.StreetNameNumber                            
                    });
					i++;
				}
			}
			List<StayDTO> sortedStayDTOs = stayDTOs.OrderBy(stayDTO => stayDTO.Price).ToList();
			return JsonSerializer.Serialize(sortedStayDTOs);
		}
		private async Task<StayOfferRequest> CreateStayOfferRequest(StayDTO stayDTO)
		{
			GeoResponse geoResponse = await _geoService.GetGeographicCoordinatesAsync(stayDTO);
			var stayOfferRequest = new StayOfferRequest
			{
				Data = new StayDataRequest
				{
					Location = new Location
					{
						Radius = 100,
						GeographicCoordinates = new GeographicCoordinates
						{
							Latitude = Double.Parse(geoResponse.Latitude, CultureInfo.InvariantCulture),
							Longitude = Double.Parse(geoResponse.Longitude, CultureInfo.InvariantCulture)
						}
					},
					CheckInDate = stayDTO.CheckInDate.ToString("yyyy-MM-dd"),
					CheckOutDate = stayDTO.CheckOutDate.ToString("yyyy-MM-dd"),
					Rooms = 1,


					Guests = new List<Guest>
					{
						new Guest
						{
							Type = "adult",
						}
					},
				}
			};
			return stayOfferRequest;
		}
	}
}
