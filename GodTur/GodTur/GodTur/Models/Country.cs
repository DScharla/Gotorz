using System.ComponentModel.DataAnnotations;

namespace GodTur.Models
{
	public class Country
	{
		[Key]
		public int CountryId { get; set; }
		public string Name { get; set; }
		public string IataCountryCode { get; set; }

		public ICollection<City> Cities { get; set; }
	}
}
