using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Auth
{

	public class CustomAuthStateProvider : AuthenticationStateProvider
	{
		private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
		private ClaimsPrincipal? _authenticated;

		public override Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			return Task.FromResult(new AuthenticationState(_authenticated ?? _anonymous));
		}

		public void MarkUserAsAuthenticated(string username)
		{
			var authenticatedUser = new ClaimsPrincipal(
				new ClaimsIdentity(new[]
				{
				new Claim(ClaimTypes.Name, username),
				}, "apiauth"));

			_authenticated = authenticatedUser;
			NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
		}

		public void MarkUserAsLoggedOut()
		{
			_authenticated = null;
			NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
		}
	}
}
