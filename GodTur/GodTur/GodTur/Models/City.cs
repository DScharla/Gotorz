using System.ComponentModel.DataAnnotations;

namespace GodTur.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }
        public string Name { get; set; }
        public string IataCountryCode { get; set; }

        public ICollection<Airport> Airports { get; set; }
        public ICollection<Hotel> Hotels { get; set; }
    }
}
