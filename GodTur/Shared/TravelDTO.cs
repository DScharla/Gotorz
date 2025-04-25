using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class TravelDTO
    {
        public FlightDTO? OutboundFlight { get; set; }
        public FlightDTO? HomeboundFlight { get; set; }
        public StayDTO? Stay { get; set; }

    }
}
