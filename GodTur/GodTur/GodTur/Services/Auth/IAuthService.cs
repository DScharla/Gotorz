using Shared;

namespace GodTur.Services.Auth
{
	public interface IAuthService
	{
		Task<AuthResponseDTO> GetAuthResponse(string username, string password);
		Task<AuthResponseDTO> VerifyAndGenerateTokenAsync(AuthResponseDTO authResponseDTO);
	}
}