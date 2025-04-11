namespace GodTur.Services
{
    public class DuffelClient
    {
        public HttpClient HttpClient { get; }
        public DuffelClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }
    }
}
