﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class StayDTO
    {        
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set;}
		public string? StreetNameNumber { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
		public double? Price { get; set; }
        public int? AvailableRooms { get; set; }
        public string? HotelName { get; set; }
        public string? Currency { get; set; }
    }



    
}
