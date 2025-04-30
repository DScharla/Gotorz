using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Airport
    {
        public string Name { get; set; }
        public string Continent { get; set; }
        public string CountryCode { get; set; }
        public string Municipality { get; set; }
        public string IATACode { get; set; }

        public override string ToString()
        {
            return $"{Name} ({IATACode}) - {Municipality}, {CountryCode}, {Continent}";
        }
        public string GetIATACode()
        {
            return IATACode;
        }
    }
}
