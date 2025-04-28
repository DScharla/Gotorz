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
        [Required]
        [DateFromNow]
        public DateTime CheckInDate { get; set; }
        [Required]
        [DateFromPlusOne]
        public DateTime CheckOutDate { get; set;}
		public string? StreetNameNumber { get; set; }
        [Required]
        [AvailableCities]
		public string City { get; set; }
        [Required]
        [AvailableCountries]
		public string Country { get; set; }
		public double? Price { get; set; }
        public int? AvailableRooms { get; set; }
        public string? HotelName { get; set; }
        public List<string> AvailableCities() 
        { 
            AvailableCitiesAttribute availableCities = new AvailableCitiesAttribute();
            return availableCities._cities;
        }

    }
}
