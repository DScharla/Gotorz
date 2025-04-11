using GodTur.Models;

namespace GodTur.Services
{
    public interface IStaysService
    {
        Task<StayOfferResponse> PostStaysAsync(StayOfferRequest stayOfferRequest);
    }
}
