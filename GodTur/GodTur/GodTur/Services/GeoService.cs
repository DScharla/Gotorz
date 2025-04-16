using GodTur.Models;
using Shared;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Net.WebRequestMethods;

namespace GodTur.Services
{
    public class GeoService : IGeoService
    {
        private readonly HttpClient _httpClient;

		public GeoService(GeoClient geoClient)
		{
			_httpClient = geoClient.HttpClient;
		}
		public async Task<GeoResponse> GetGeographicCoordinatesAsync(StayDTO stayDTO)
        {
			var options = new JsonSerializerOptions
			{
				DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
				WriteIndented = true
			};
			string url = "search?";
            string query = $"q={stayDTO.City},{stayDTO.Country}";
            string format = "&limit=1&format=json";

            string requestUrl = url + query + format;
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(requestUrl);
            if(httpResponseMessage.IsSuccessStatusCode)
            {
                if (httpResponseMessage.Content != null)
                {
                    string responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
					var geoResponses = JsonSerializer.Deserialize<List<GeoResponse>>(responseContent);

                    return geoResponses.FirstOrDefault();                  
                } 
                else
                {
                    throw new Exception("No content in response");
                }
            }
            else
            {
                throw new Exception($"API request failed: {httpResponseMessage.StatusCode}");
            }
        }
    }
}
