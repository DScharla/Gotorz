using System.ComponentModel.DataAnnotations;

namespace Shared.ValidationAttributes
{
    internal class DateFromPlusOneAttribute : ValidationAttribute
    {
        private readonly DateTime _dateTime = DateTime.Now.AddDays(1);
        public DateFromPlusOneAttribute() { }
        public string GetErrorMessage() => $"Date must be past {_dateTime}";
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var date = (DateTime)value;

            if (DateTime.Compare(date, DateTime.Now.AddDays(1)) < 0) return new ValidationResult(GetErrorMessage());
            else return ValidationResult.Success;
        }
    }

}

