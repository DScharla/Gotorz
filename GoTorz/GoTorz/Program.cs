using GoTorz.Components;
using GoTorz.Services;
using System.Net.Http;
using System.Text.Json;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;

namespace GoTorz
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services
                .AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddScoped<OfferService>
                (
                sp =>
                    {
                        var httpClient = new HttpClient();
                        httpClient.BaseAddress = new Uri(
                            builder.Configuration["HttpClients:DuffelClientURI"] ?? "Forkert URI"
                            );
                        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                        httpClient.DefaultRequestHeaders.Add("Duffel-Version", "v2");
                        httpClient.DefaultRequestHeaders.Add("Authorization", builder.Configuration["APIKeys:DuffelKey"] ?? "Forkert Key");
                        return new OfferService(httpClient);
                    }
                );
            builder.Services.AddBlazorise(options => { options.Immediate = true; })
	                        .AddBootstrapProviders()
	                        .AddFontAwesomeIcons();
			var app = builder.Build();

            var scope = app.Services.CreateScope();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
