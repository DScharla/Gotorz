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
            builder.Services.AddScoped<OfferService>(
                sp =>
                {
                    HttpClient httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri(builder.Configuration["ConnectionStrings:DuffelAddress"]);
                    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                    httpClient.DefaultRequestHeaders.Add("Duffel-Version", "v2");
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {builder.Configuration["ApiKeys:DuffelTest"]}");
                    return new OfferService(httpClient);
                }
                );
            builder.Services.AddHttpClient<OfferService>();
            var app = builder.Build();

            var scope = app.Services.CreateScope();
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
