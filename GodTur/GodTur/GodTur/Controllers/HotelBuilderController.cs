using System.Text.Json;
using GodTur.Models;
using GodTur.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace GodTur.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HotelBuilderController : ControllerBase
	{
		IStaysService? _staysService;
		public HotelBuilderController(IStaysService staysService)
		{

			_staysService = staysService;
		}

		[HttpPost, Route("stays")]
		public async Task<string> Stays([FromBody] StayDTO stayDTO)
		{
			StayDTO stayParam = stayDTO;


			List<StayDTO> stayDTOs = new List<StayDTO>();
			StayOfferRequest stayOfferRequest = CreateStayOfferRequest(stayParam);

			if (_staysService is not null)
			{
				StayOfferResponse stayOfferResponse = await _staysService.PostStaysAsync(stayOfferRequest);
				foreach (var accommodation in stayOfferResponse.Data.Accommodations)
				{
					stayDTOs.Add(new StayDTO
					{
						Hvad = accommodation.,
						Vil = accommodation.,
						Vi = accommodation.,
						Ha = accommodation.,
					});
				}
			}
			stayDTOs.OrderBy(f => f.PropertyTilSort);
			return JsonSerializer.Serialize(stayDTOs);
		}
		private StayOfferRequest CreateStayOfferRequest(StayDTO stayDTO)
		{
			var stayOfferRequest = new StayOfferRequest
			{
				Data = new StayDataRequest
				{
					Location = new Location
					{
						Radius = stayDTO.Location.Radius,
						GeographicCoordinates = new GeographicCoordinates
						{
							Latitude = stayDTO.Location.GeographicCoordinates.Latitude,
							Longitude = stayDTO.Location.GeographicCoordinates.Longitude,
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
