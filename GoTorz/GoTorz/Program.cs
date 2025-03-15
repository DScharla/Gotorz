using GoTorz.Components;
using GoTorz.Services;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication;
using GoTorz.Model;
using Microsoft.AspNetCore.Components.Web;

namespace GoTorz
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddServerSideBlazor(options =>
            {
                options.RootComponents.RegisterForJavaScript<Search>(identifier: "search",
                javaScriptInitializer: "myFucntion");
            });

            builder.Services.AddScoped<OfferService>(sp =>
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(builder.Configuration["ApiSettings:DuffelBaseUrl"] ?? throw new InvalidOperationException("DuffelBaseUrl is not configured."))
                };
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Duffel-Version", "v2");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {builder.Configuration["AuthSettings:DuffelApiKey"]}");
                return new OfferService(client);
            });
            builder.Services.AddScoped<OfferResponse>();
            builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
            builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddOpenIdConnect("Local", options =>
                {
                    // Bind the "Local" section from appsettings.json to the options.
                    builder.Configuration.Bind("Local", options);
                    options.SaveTokens = true;
                    // (Optional) Additional event handlers...
                    options.Events = new OpenIdConnectEvents
                    {
                        OnRedirectToIdentityProvider = context =>
                        {
                            // You can log or inspect the request here.
                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context =>
                        {
                            // Log or handle authentication failure.
                            return Task.CompletedTask;
                        }
                    };
                });

            builder.Services.AddAuthorization();
            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAntiforgery();

            app.MapGet("/authentication/login", async (HttpContext context) =>
            {
                // Trigger the login challenge
                await context.ChallengeAsync("Local", new AuthenticationProperties { RedirectUri = "/" });
            });

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
