using Client.Pages;
using GodTur.Components;
using GodTur.Controllers;
using GodTur.Services;
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
        builder.Services.AddHttpClient<DuffelClient>(client =>
        {
            client.BaseAddress = new Uri(
                builder.Configuration["HttpClients:DuffelClientURI"] ?? "https://api.duffel.com/");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Duffel-Version", "v2");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + 
                builder.Configuration["APIKeys:DuffelKey"] ?? "Bearer MISSING_KEY");
        });
        builder.Services.AddHttpClient<GeoClient>(client =>
        {
            client.BaseAddress = new Uri(
                   builder.Configuration["HttpClients:GeoClientURI"] ?? "https://nominatim.openstreetmap.org/");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("User-Agent", "GodTur");
        });

			builder.Services.AddScoped<IOfferService, OfferService>();
        builder.Services.AddScoped<IStaysService, StaysService>();
        builder.Services.AddScoped<IGeoService, GeoService>();

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

		app.MapControllers(); // se om dette er nok ellers m� vi �ndre p� det.

		app.Run();
    }
}
