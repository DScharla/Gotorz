using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class TravelPackageDTO
    {
        public StayDTO PackageHotel { get; set; }
        public FlightDTO OutboundFlight { get; set; }
        public FlightDTO ReturnFlight { get; set; }

    }
}
