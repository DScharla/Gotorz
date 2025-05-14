using System.ComponentModel.DataAnnotations;

namespace Shared.ValidationAttributes
{
    internal class DateFromPlusOneAttribute : ValidationAttribute
    {
        /*private readonly DateTime _dateTime = DateTime.Now.AddDays(1);
        private readonly DateTime compareDateTime = DateTime.Now;
        public DateFromPlusOneAttribute(string dateTimeToCompare) 
        {
            compareDateTime = DateTime.Parse(dateTimeToCompare);    
        }
        public string GetErrorMessage() => $"Date must be past {_dateTime}";
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var date = (DateTime)value;

            if (DateTime.Compare(date, compareDateTime) < 0) return new ValidationResult(GetErrorMessage());
            else return ValidationResult.Success;
        }*/

        //Indsat fra LLM
        private readonly string _comparisonProperty;

        public DateFromPlusOneAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult($"Value cannot be null");
            }
            var currentValue = (DateTime)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (property == null)
            {
                return new ValidationResult($"Unknown property: {_comparisonProperty}");
            }

            var comparisonValue = (DateTime)property.GetValue(validationContext.ObjectInstance);

            var minAllowedDate = comparisonValue.AddDays(1);

            if (currentValue < minAllowedDate)
            {
                return new ValidationResult(
                    $"{validationContext.DisplayName} Skal være mindst en dag efter {_comparisonProperty}");
            }

            return ValidationResult.Success;
        }
    }

}

