namespace GodTur.Services
{
	public class GeoClient
	{
		public HttpClient HttpClient { get; }
		public GeoClient(HttpClient httpClient)
		{
			HttpClient = httpClient;
		}
	}
}
