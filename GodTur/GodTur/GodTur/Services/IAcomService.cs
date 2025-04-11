namespace GodTur.Services
{
    public interface IAcomService
    {
        Task<AcomResponse> PostAcommodationAsync(AcomRequest acomRequest);
    }
}
