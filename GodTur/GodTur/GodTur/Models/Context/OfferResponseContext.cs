using Microsoft.EntityFrameworkCore;

namespace GodTur.Models.Context
{
	public class OfferResponseContext : DbContext
	{
		public DbSet<Offer> FlightOffers { get; set; }
		public DbSet<FlightDetail> Flights { get; set; }
		public DbSet<Segment> Segments { get; set; }
		public DbSet<Stop> Stops { get; set; }
		public DbSet<PassengerResponse> Passengers { get; set; }
		public DbSet<Airport> Airports { get; set; }
		public DbSet<City> Cities { get; set; }
	}
}
