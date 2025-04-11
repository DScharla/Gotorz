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
		StaysService? _staysService;
		public HotelBuilderController(StaysService staysService)
		{

			_staysService = staysService;
		}

		[HttpPost, Route("stays")]
		public async Task<string> Stays([FromBody] StayDTO stayDTO)
		{
			StayDTO stayParam = stayDTO;


			List<StayDTO> stayDTOs = new List<StayDTO>();
			StayRequest stayRequest = CreateStayRequest(stayParam);

			if (_staysService is not null)
			{
				StayResponse stayResponse = await _staysService.PostStayAsync(stayRequest);
				foreach (var stay in stayResponse.Data.Results)
				{
					stayDTOs.Add(new StayDTO
					{
						Hvad = stay.,
						Vil = stay.,
						Vi = stay.,
						Ha = stay.,
					});
				}
			}
			stayDTOs.OrderBy(f => f.PropertyTilSort);
			return JsonSerializer.Serialize(stayDTOs);
		}
		private StayRequest CreateStayRequest(StayDTO stayDTO)
		{
			var stayRequest = new StayRequest
			{
				Data = new DataRequest
				{
					Result = new List<ResultRequest>
					{
						new ResultRequest
						{
							Hvad = stayDTO.,
							Vil = stayDTO.,
							Vi = stayDTO.,
						}
					},
					Guests = new List<GuestRequest>
					{
						new GuestRequest
						{
							Type = "adult",
						}
					},
				}
			};
			return StayRequest;
		}
	}
}
