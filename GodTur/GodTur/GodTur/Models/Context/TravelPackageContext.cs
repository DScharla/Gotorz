using Microsoft.EntityFrameworkCore;

namespace GodTur.Models.Context
{
	public class TravelPackageContext : DbContext
	{
		// https://www.c-sharpcorner.com/article/migration-in-code-first-approach/
		// Ovenstående hjemmeside er brugt til inspirationn og guide for steps i EF core migrationen,
		// gennem Package Manager Console. Testet resultat ud fra en lokal DB hvor appsettings er sat op,
        // til en eksisterende db med en bruger.

		// Commands:
		// 1. Add-Migration -Context OfferResponseContext
		// 2. Update-Database -Context OfferResponseContext
		public TravelPackageContext(DbContextOptions<TravelPackageContext> options)
          : base(options)
        {
        }
		public DbSet<Country> Countries { get; set; }
		public DbSet<City> Cities { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<TravelPackage> TravelPackages { get; set; }

        //Kontrakt til hvordan alle decimal håndteres til databasen.
		protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
		{
			configurationBuilder
				.Properties<decimal>()
				.HavePrecision(18, 2);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfigurer en-til-mange relation mellem City og Airport
            modelBuilder.Entity<Airport>()
                .HasOne(a => a.City)
                .WithMany(c => c.Airports)
                .HasForeignKey(a => a.CityId);

            // Seeder Databasen med lande.
			modelBuilder.Entity<Country>().HasData(
	            new Country { CountryId = 1, Name = "Denmark", IataCountryCode = "DK" },
	            new Country { CountryId = 2, Name = "Germany", IataCountryCode = "DE" },
	            new Country { CountryId = 3, Name = "France", IataCountryCode = "FR" }
                );

//			--Add cities
//INSERT INTO Cities(Name, CountryId)
//VALUES
//('Copenhagen', (SELECT CountryId FROM Countries WHERE Name = 'Denmark')),
//('Aalborg', (SELECT CountryId FROM Countries WHERE Name = 'Denmark'));

//			--Add airports
//			INSERT INTO Airports(Name, IataCode, CityId)
//VALUES
//('Copenhagen Airport', 'CPH', (SELECT CityId FROM Cities WHERE Name = 'Copenhagen')),
//('AAlborg Airport', 'AAL', (SELECT CityId FROM Cities WHERE Name = 'Aalborg'));


			// Konfigurer en-til-mange relationer for Flight
			modelBuilder.Entity<Flight>()
                .HasOne(f => f.OriginAirport)
                .WithMany(a => a.OriginFlights)
                .HasForeignKey(f => f.OriginAirportId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Flight>()
                .HasOne(f => f.DestinationAirport)
                .WithMany(a => a.DestinationFlights)
                .HasForeignKey(f => f.DestinationAirportId)
                .OnDelete(DeleteBehavior.Restrict);

            // Konfigurer en-til-mange relationer for Hotel
            modelBuilder.Entity<Hotel>()
                .HasOne(a => a.City)
                .WithMany(c => c.Hotels)
                .HasForeignKey(a => a.CityId);

            // Konfigurer en-til-mange relationer for TravelPackage
            modelBuilder.Entity<TravelPackage>()
                .HasOne(tp => tp.OutboundFlight)
                .WithMany(f => f.OutboundTravelPackages)
                .HasForeignKey(tp => tp.OutboundFlightId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TravelPackage>()
                .HasOne(tp => tp.ReturnFlight)
                .WithMany(f => f.ReturnTravelPackages)
                .HasForeignKey(tp => tp.ReturnFlightId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TravelPackage>()
                .HasOne(tp => tp.PackageHotel)
                .WithMany(h => h.HotelTravelPackages)
                .HasForeignKey(tp => tp.PackageHotelId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
