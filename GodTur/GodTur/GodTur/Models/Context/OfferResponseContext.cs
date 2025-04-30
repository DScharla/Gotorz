using Microsoft.EntityFrameworkCore;

namespace GodTur.Models.Context
{
	public class OfferResponseContext : DbContext
	{
        public OfferResponseContext(DbContextOptions<OfferResponseContext> options)
          : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<TravelPackage> TravelPackages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfigurer en-til-mange relation mellem City og Airport
            modelBuilder.Entity<Airport>()
                .HasOne(a => a.City)
                .WithMany(c => c.Airports)
                .HasForeignKey(a => a.CityId);

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
        }
    }
}
