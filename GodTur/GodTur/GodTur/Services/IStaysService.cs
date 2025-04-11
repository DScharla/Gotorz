namespace GodTur.Services
{
    public interface IStaysService
    {
        Task<AcomResponse> PostStaysAsync(StaysRequest acomRequest);
    }
}
