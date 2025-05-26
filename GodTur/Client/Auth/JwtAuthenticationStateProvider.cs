using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Client.Models;
using Client.Services;

namespace Client.Auth
{
	public class JwtAuthenticationStateProvider : AuthenticationStateProvider
	{
		private readonly ILocalStorageService _localStorage;
		private readonly HttpClient _httpClient;

		public JwtAuthenticationStateProvider(
			ILocalStorageService localStorage,
			HttpClient httpClient)
		{
			_localStorage = localStorage;
			_httpClient = httpClient;
		}

		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			var savedToken = await _localStorage.GetItemAsync<JwtAuthenticationState>("authToken");

			if (savedToken == null || string.IsNullOrWhiteSpace(savedToken.Token) ||
				savedToken.Expiration <= DateTime.Now)
			{
				return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
			}

			_httpClient.DefaultRequestHeaders.Authorization =
				new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", savedToken.Token);

			return new AuthenticationState(
				new ClaimsPrincipal(
					new ClaimsIdentity(ParseClaimsFromJwt(savedToken.Token), "jwt")));
		}

		public void NotifyUserAuthentication(string token)
		{
			var authenticatedUser = new ClaimsPrincipal(
				new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt"));

			var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
			NotifyAuthenticationStateChanged(authState);
		}

		public void NotifyUserLogout()
		{
			var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
			var authState = Task.FromResult(new AuthenticationState(anonymousUser));
			NotifyAuthenticationStateChanged(authState);
		}

		private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
		{
			var handler = new JwtSecurityTokenHandler();
			var token = handler.ReadJwtToken(jwt);
			return token.Claims;
		}
	}
}