using System.Net.Http.Json;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Authorization;
using Client.Auth;
using Client.Models;

namespace Client.Services
{
	public class AuthService : IAuthService
	{
		private readonly HttpClient _httpClient;
		private readonly AuthenticationStateProvider _authStateProvider;
		private readonly ILocalStorageService _localStorage;

		public AuthService(
			HttpClient httpClient,
			AuthenticationStateProvider authStateProvider,
			ILocalStorageService localStorage)
		{
			_httpClient = httpClient;
			_authStateProvider = authStateProvider;
			_localStorage = localStorage;
		}

		public async Task<bool> LoginAsync(string username, string password)
		{
			try
			{
				var loginRequest = new LoginRequest
				{
					Username = username,
					Password = password
				};

				// Replace with your actual API endpoint
				var response = await _httpClient.PostAsJsonAsync("api/Authentication/Login", loginRequest);

				if (response.IsSuccessStatusCode)
				{
					var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();

					if (authResponse != null && !string.IsNullOrEmpty(authResponse.Token))
					{
						// Store token in local storage
						await _localStorage.SetItemAsync("authToken", new JwtAuthenticationState
						{
							Token = authResponse.Token,
							Expiration = authResponse.Expiration
						});

						// Update auth state
						((JwtAuthenticationStateProvider)_authStateProvider).NotifyUserAuthentication(authResponse.Token);

						// Set auth header for future requests
						_httpClient.DefaultRequestHeaders.Authorization =
							new AuthenticationHeaderValue("Bearer", authResponse.Token);

						return true;
					}
				}

				return false;
			}
			catch
			{
				return false;
			}
		}

		public async Task LogoutAsync()
		{
			// Remove token from local storage
			await _localStorage.RemoveItemAsync("authToken");

			// Update auth state
			((JwtAuthenticationStateProvider)_authStateProvider).NotifyUserLogout();

			// Remove auth header
			_httpClient.DefaultRequestHeaders.Authorization = null;
		}

		public async Task<bool> IsAuthenticatedAsync()
		{
			var authState = await _authStateProvider.GetAuthenticationStateAsync();
			return authState.User.Identity?.IsAuthenticated ?? false;
		}
	}
}
