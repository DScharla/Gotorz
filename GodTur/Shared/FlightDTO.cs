using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public class FlightDTO
    {

        public int ID { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public string Origin { get; set; }
        public string? OriginIata { get; set; }
        public string Destination { get; set; }
        public string? DestinationIata { get; set; }
        public decimal? Price { get; set; }
        public string? FlightNumber { get; set; }

    }
}
