using Client.Pages;
using GodTur.Components;
using GodTur.Controllers;
using GodTur.Services;
using Microsoft.AspNetCore.Cors;
using GodTur.Models.Context;
using Microsoft.EntityFrameworkCore;
using GodTur.Middleware;
using GodTur.Models.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using GodTur.Services.Auth;


namespace GodTur;

public class Program
{
	public static async Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddRazorComponents()
			.AddInteractiveServerComponents()
			.AddInteractiveWebAssemblyComponents();
		builder.Services.AddDbContext<TravelPackageContext>(options =>
			options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
		);
		builder.Services.AddDbContext<UserDbContext>(options =>
		options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
		);

		builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
			.AddEntityFrameworkStores<UserDbContext>()
			.AddDefaultTokenProviders();

		builder.Services.AddJwtAuthentication(builder.Configuration);

		builder.Services.AddCors(options =>
		{
			options.AddPolicy("AllowClient", builder => builder.WithOrigins("https://localhost:7177")
			.AllowAnyHeader()
			.AllowAnyMethod()
			.AllowCredentials()
			);

		});
		builder.Services.AddControllers();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
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
		builder.Services.AddScoped<ITravelPackageDBService, TravelPackageDBService>();
		builder.Services.AddScoped<IAuthService, AuthService>();
		
		builder.Services.Configure<PasswordHasherOptions>(options =>
		{
			options.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3;
		});
		var app = builder.Build();
		app.UseRouting();
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
		app.UseSwagger();
		app.UseSwaggerUI();

		app.UseHttpsRedirection();

		app.UseStaticFiles();
		//app.UseAntiforgery();


		app.MapRazorComponents<App>()
			.AddInteractiveServerRenderMode()
			.AddInteractiveWebAssemblyRenderMode()
			.AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

		app.MapControllers(); // se om dette er nok ellers m� vi �ndre p� det.
		await UserDbInitializer.SeedRolesToDb(app);
		await UserDbInitializer.SeedUserToDb(app);
		app.Run();
	}
}
