using Client.Auth;
using Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");
		builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
		builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
		builder.Services.AddScoped<IAuthService, AuthService>();

        builder.Services.AddAuthorizationCore();

		builder.Services.AddScoped<AuthorizationMessageHandler>();

		builder.Services.AddScoped(sp =>
		{
			var handler = sp.GetRequiredService<AuthorizationMessageHandler>();
			handler.InnerHandler = new HttpClientHandler();

			return new HttpClient(handler)
			{
				BaseAddress = new Uri("https://localhost:7112/")
			};
		});

		await builder.Build().RunAsync();
    }
}
