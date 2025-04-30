using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GodTur.Models
{
    public class Flight
    {
        [Key]
        public int FlightId { get; set; }

        [ForeignKey("OriginAirport")]
        public string OriginAirportId { get; set; }
        public Airport OriginAirport { get; set; }

        [ForeignKey("DestinationAirport")]
        public string DestinationAirportId { get; set; }
        public Airport DestinationAirport { get; set; }

        public DateTime DepartingAt { get; set; }
        public DateTime ArrivingAt { get; set; }

        public string TotalAmount { get; set; }
        public string TotalCurrency { get; set; }
        public string FlightNumber { get; set; }

        public ICollection<TravelPackage> OutboundTravelPackages { get; set; }
        public ICollection<TravelPackage> ReturnTravelPackages { get; set; }
    }
}
