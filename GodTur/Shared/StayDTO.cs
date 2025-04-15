using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class StayDTO
    {
        public LocationDTO Location { get; set; } 
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set;}
        public StayAdressDTO StayAdress { get; set; }
        public double? Price { get; set; }
        public int? AvailableRooms { get; set; }
        public string? HotelName { get; set; }
    }
    public class StayAdressDTO
    {
        public string? StreetName { get; set; }
        public string City { get; set; }
        public string? Country { get; set; }
        public string? StreetNumber { get; set; }

    }
    public class LocationDTO
    {
		public int Radius { get; set; }

		public GeographicCoordinatesDTO GeographicCoordinates { get; set; }
	}
    public class GeographicCoordinatesDTO
    {
		public double Latitude { get; set; }
		public double Longitude { get; set; }
	}
}
