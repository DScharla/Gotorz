using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ValidationAttributes
{
    internal class AvailableAirportsAttribute : ValidationAttribute
    {
        internal readonly List<Airport> _airports = new();
        public AvailableAirportsAttribute()
        {
            _airports = AvailableAirports();
            System.Diagnostics.Debug.WriteLine("Airports loaded");
            System.Diagnostics.Debug.WriteLine(_airports.Count);
            System.Diagnostics.Debug.WriteLine(_airports[0].ToString());
        }

        public string GetErrorMessage() => $"Ugyldig lufthavn. vælg en fra listen - vores autocomplete er awesome";
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var airports = (string)value;

            if (!IsValidAirport(airports)) return new ValidationResult(GetErrorMessage());
            else return ValidationResult.Success;
        }

        private List<Airport> AvailableAirports()
        {
            return ImportAirportsFromCSV();
        }
        private bool IsValidAirport(string airport)
        {
            return _airports.Any(a =>
                    (a.CountryCode?.ToUpperInvariant() == airport) ||
                    (a.IATACode?.ToUpperInvariant() == airport)
                );
        }
        private List<Airport> ImportAirportsFromCSV()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            //string parentDirectory = Directory.GetParent(currentDirectory)?.FullName;
            string filePath = /*C:\Users\dscha\Documents\Team15Gotorz\GodTur\Shared\airports.csv;*/ Path.Combine(currentDirectory, "airports.csv"); //hardcoded path for now
            List<Airport> airports = new();
            var lines = File.ReadAllLines(@"C:\Users\dscha\Documents\Team15Gotorz\GodTur\Shared\airports.csv");

            for (int i = 1; i < lines.Length; i++) //Starter fra 1 for a springe header over
            {
                var columns = lines[i].Split(',');

                Airport airport = new Airport
                {
                    Name = columns[3],
                    Continent = columns[7],
                    CountryCode = columns[8],
                    Municipality = columns[10],
                    IATACode = columns[13]
                };

                airports.Add(airport);
            }

            return airports;
        }
    }
}
