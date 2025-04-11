using GodTur.Models;

namespace GodTur.Services
{
    public interface IOfferService
    {
        Task<OfferResponse> PostOfferAsync(OfferRequest offerRequest);
    }
}
