using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GodTur.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }
        public string Name { get; set; }
		
        [ForeignKey("Country")]
		public int CountryId { get; set; }
		public Country Country { get; set; }

		public ICollection<Airport> Airports { get; set; }
        public ICollection<Hotel> Hotels { get; set; }
    }
}
