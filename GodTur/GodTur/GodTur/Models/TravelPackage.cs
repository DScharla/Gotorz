﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GodTur.Models
{
    public class TravelPackage
    {
        [Key]
        public int TravelPackageId { get; set; }

        [ForeignKey("OutboundFlight")]
        public int OutboundFlightId { get; set; }
        public Flight OutboundFlight { get; set; }

        [ForeignKey("ReturnFlight")]
        public int ReturnFlightId { get; set; }
        public Flight ReturnFlight { get; set; }

        [ForeignKey("PackageHotel")]
        public int PackageHotelId { get; set; }
        public Hotel PackageHotel { get; set; }
        public decimal PackagePrice { get; set; }
    }
}
