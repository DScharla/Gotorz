namespace Shared
{
    public class FlightDTO
    {
        public DateTime DepartureDate { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public double? Price { get; set; }
        public string? FlightNumber { get; set; }



    }
}
