using Client.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace Client.Models
{
    public class StayInput
    {
        public int ID { get; set; }
        [Required]
        [DateFromNow]
        public DateTime CheckInDate { get; set; }
        [Required]
        [DateFromPlusOne("CheckInDate")]
        public DateTime CheckOutDate { get; set; }
        [Required]
        [AvailableCities]
        public string City { get; set; }
        [Required]
        [AvailableCountries]
        public string Country { get; set; }

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
