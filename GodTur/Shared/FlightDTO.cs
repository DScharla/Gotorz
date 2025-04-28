using Shared.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public class FlightDTO
    {
        /*        public DateTime DepartureDate { get; set; }
                public string Origin { get; set; }
                public string Destination { get; set; }
                public double? Price { get; set; }
                public string? FlightNumber { get; set; }*/
        public int ID { get; set; }
        [Required]
        [DateFromNow]
        public DateTime? DepartureDate { get; set; }
        [Required]
        [DateFromPlusOne]
        public DateTime? ReturnDate { get; set; }
        [Required]
        [AvailableCities]
        public string Origin { get; set; }
        [Required]
        [AvailableCountries]
        public string Destination { get; set; }
        public double? Price { get; set; }
        public string? FlightNumber { get; set; }

    }
}
