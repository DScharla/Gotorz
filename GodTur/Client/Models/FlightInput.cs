using Client.ValidationAttributes;
using Shared;
using System.ComponentModel.DataAnnotations;

namespace Client.Models
{
    public class FlightInput
    {
        public string? Id { get; set; }
        [Required]
        [DateFromNow]
        public DateTime DepartureDate { get; set; }
        [Required]
        [AvailableAirports]
        public string Origin { get; set; }
        [Required]
        [DateFromPlusOne("DepartureDate")]
        public DateTime ReturnDate { get; set; }
        [Required]
        [AvailableAirports]
        public string Destination { get; set; }

        public List<Airport> AvailableAirports()
        {
            AvailableAirportsAttribute availableAirports = new AvailableAirportsAttribute();
            return availableAirports._airports;
        }
    }
}
