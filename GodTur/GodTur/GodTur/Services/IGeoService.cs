using GodTur.Models;
using Shared;
namespace GodTur.Services
{
    public interface IGeoService
    {
        public Task<GeoResponse> GetGeographicCoordinatesAsync(StayDTO stayDTO);
        
    }
}
