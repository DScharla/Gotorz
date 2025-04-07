using Client.Pages;
using GodTur.Components;
using GodTur.Controllers;
using Microsoft.AspNetCore.Cors;

namespace GodTur;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowClient", builder => builder.WithOrigins("https://localhost:7177")
            .AllowAnyHeader()
            .AllowAnyMethod()
            );

        });
        builder.Services.AddControllers();
/*		builder.Services.AddScoped<FlightBuilderController>
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
					return new FlightBuilderController(httpClient);
				}
				);*/
        
		var app = builder.Build();
        app.UseCors("AllowClient");
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

		app.UseStaticFiles();
        app.UseAntiforgery();


		app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

		app.MapControllers(); // se om dette er nok ellers må vi ændre på det.

		app.Run();
    }
}
