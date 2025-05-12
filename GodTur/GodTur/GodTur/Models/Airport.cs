using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GodTur.Models
{
    public class Airport
    {
        [Key]
        public int AirportId { get; set; }
        public string Name { get; set; }
        public string IataCode { get; set; }

        [ForeignKey("City")]
        public int CityId { get; set; }
        public City City { get; set; }

        public ICollection<Flight> OriginFlights { get; set; }
        public ICollection<Flight> DestinationFlights { get; set; }
    }
}
