using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GodTur.Models
{
    public class Flight
    {
        [Key]
        public int FlightId { get; set; }

        [ForeignKey("OriginAirport")]
        public int OriginAirportId { get; set; }
        public Airport OriginAirport { get; set; }

        [ForeignKey("DestinationAirport")]
        public int DestinationAirportId { get; set; }
        public Airport DestinationAirport { get; set; }

        public DateTime DepartingAt { get; set; }
        public DateTime ArrivingAt { get; set; }

        public decimal FlightPrice { get; set; }
        public string FPCurrency { get; set; }
        public string FlightNumber { get; set; }

        public ICollection<TravelPackage> OutboundTravelPackages { get; set; }
        public ICollection<TravelPackage> ReturnTravelPackages { get; set; }
    }
}
