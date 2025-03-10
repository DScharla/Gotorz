using GoTorz.Components;
using GoTorz.Services;
using System.Net.Http;
using System.Text.Json;

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
            builder.Services.AddScoped<OfferService>(sp =>
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(builder.Configuration["ApiSettings:DuffelBaseUrl"] ?? throw new InvalidOperationException("DuffelBaseUrl is not configured."))
                };
                client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Duffel-Version", "v2");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {builder.Configuration["AuthSettings:DuffelApiKey"]}");
                return new OfferService(client);
            });
            var app = builder.Build();

            //var offerService = scope.ServiceProvider.GetRequiredService<OfferService>();

            //try
            //{
            //    var response = offerservice.postofferasync();
            //    console.writeline(jsonserializer.serialize(response, new jsonserializeroptions { writeindented = true }));
            //}
            //catch (exception ex)
            //{
            //    console.writeline($"fejl: {ex.message}");
            //}

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
