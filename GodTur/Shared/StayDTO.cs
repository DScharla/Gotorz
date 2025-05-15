using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Shared.ValidationAttributes;

namespace Shared
{
    public class StayDTO
    {        
        public int ID { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set;}
		public string? StreetNameNumber { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
		public decimal? Price { get; set; }
        public string? Currency { get; set; }
        public int? AvailableRooms { get; set; }
        public string? HotelName { get; set; }
        public List<string> AvailableCities() 
        { 
            AvailableCitiesAttribute availableCities = new AvailableCitiesAttribute();
            return availableCities._cities;
        }

        public List<string> AvailableCountries()
        {
            AvailableCountriesAttribute availableCountries = new AvailableCountriesAttribute();
            return availableCountries._countries;
        }

    }
}
