using Client.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Client.ValidationAttributes
{
    internal class AvailableAirportsAttribute : ValidationAttribute
    {
        internal readonly List<Airport> _airports = new();
        public AvailableAirportsAttribute()
        {
            _airports = GetAvailableAirports();
        }

        private string GetErrorMessage() => $"Ugyldig lufthavn. vælg en fra listen - vores autocomplete er awesome";
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult(GetErrorMessage());
            var airports = (string)value;

            if (!IsValidAirport(airports)) return new ValidationResult(GetErrorMessage());
            else return ValidationResult.Success;
        }

        private List<Airport> GetAvailableAirports()
        {
            List<Airport> airports = new List<Airport>{
                new Airport { Name = "Copenhagen Airport", IATACode = "CPH", Continent = "Europe", CountryCode = "DK", Municipality = "Copenhagen" },
                new Airport { Name = "Billund Airport", IATACode = "BLL", Continent = "Europe", CountryCode = "DK", Municipality = "Billund" },
                new Airport { Name = "Aalborg Airport", IATACode = "AAL", Continent = "Europe", CountryCode = "DK", Municipality = "Aalborg" },
                new Airport { Name = "JFK Airport", IATACode = "JFK", Continent = "North America", CountryCode = "UD", Municipality = "New York" },
                new Airport { Name = "Los Angeles International Airport", IATACode = "LAX", Continent = "North America", CountryCode = "US", Municipality = "Los Angeles" },
                new Airport { Name = "Chicago O'Hare International Airport", IATACode = "ORD", Continent = "North America", CountryCode = "US", Municipality = "Chicago" },
                new Airport { Name = "Houston George Bush Intercontinental Airport", IATACode = "IAH", Continent = "North America", CountryCode = "US", Municipality = "Houston" },
                new Airport { Name = "Phoenix Sky Harbor International Airport", IATACode = "PHX", Continent = "North America", CountryCode = "US", Municipality = "Phoenix" },
            };//ImportAirportsFromCSV(); Virker ikke pt
            /*List<string> airportStrings = new List<string>();
            foreach (Airport airport in airports) 
            {
                airportStrings.Add(airport.ToString());
            }*/
            return airports;
        }
        private bool IsValidAirport(string airport)
        {
            return _airports.Any(a =>
            a.IATACode.Contains(airport)
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
