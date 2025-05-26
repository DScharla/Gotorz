using System.Net.Http.Headers;
using Client.Models;
using Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Client.Auth
{
	public class AuthorizationMessageHandler : DelegatingHandler
	{
		private readonly ILocalStorageService _localStorage;
		private readonly NavigationManager _navigationManager;

		public AuthorizationMessageHandler(
			ILocalStorageService localStorage,
			NavigationManager navigationManager)
		{
			_localStorage = localStorage;
			_navigationManager = navigationManager;
		}

		protected override async Task<HttpResponseMessage> SendAsync(
			HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var savedToken = await _localStorage.GetItemAsync<JwtAuthenticationState>("authToken");

			if (savedToken != null && !string.IsNullOrEmpty(savedToken.Token) &&
				savedToken.Expiration > DateTime.Now)
			{
				request.Headers.Authorization =
					new AuthenticationHeaderValue("Bearer", savedToken.Token);
			}

			var response = await base.SendAsync(request, cancellationToken);

			if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
			{
				await _localStorage.RemoveItemAsync("authToken");
				_navigationManager.NavigateTo("/login");
			}

			return response;
		}
	}
}
