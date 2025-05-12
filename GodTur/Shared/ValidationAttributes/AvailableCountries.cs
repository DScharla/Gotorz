using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ValidationAttributes
{
    internal class AvailableCountriesAttribute : ValidationAttribute
    {
        internal readonly List<string> _countries = new List<string>();
        public AvailableCountriesAttribute()
        {
            _countries = AvailableCountries();
        }

        private string GetErrorMessage() => $"Ugyldig land. vælg et fra listen - vores autocomplete er awesome";
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var country = (string)value;

            if (!IsValidCountry(country)) return new ValidationResult(GetErrorMessage());
            else return ValidationResult.Success;
        }

        private List<string> AvailableCountries()
        {
            return new List<string>
            {
                "Denmark",
                "United States",
            };
        }
        private bool IsValidCountry(string country)
        {
            return _countries.Contains(country);
        }
    }
}
