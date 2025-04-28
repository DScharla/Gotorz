using System.ComponentModel.DataAnnotations;

namespace Shared.ValidationAttributes
{
    internal class AvailableCitiesAttribute : ValidationAttribute
    {
        internal readonly List<string> _cities = new List<string>();
        public AvailableCitiesAttribute() 
        {
            _cities = AvailableCities();
        }

        public string GetErrorMessage() => $"Ugyldig by. vælg en fra listen - vores autocomplete er awesome";
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var city = (string)value;

            if (!IsValidCity(city)) return new ValidationResult(GetErrorMessage());
            else return ValidationResult.Success;
        }

        private List<string> AvailableCities()
        {
            return new List<string>
            {
                "New York",
                "Los Angeles",
                "Chicago",
                "Houston",
                "Phoenix",
                "Philadelphia",
                "San Antonio",
                "San Diego",
                "Dallas",
                "San Jose"
            };
        }
        private bool IsValidCity(string city)
        {
            return _cities.Contains(city);
        }
    }

}
