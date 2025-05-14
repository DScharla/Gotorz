using System.ComponentModel.DataAnnotations;

namespace Shared.ValidationAttributes
{
    internal class DateFromNowAttribute : ValidationAttribute
    {
        private readonly DateTime _dateTime = DateTime.Now;
        public DateFromNowAttribute() { }
        public string GetErrorMessage() => $"Date must be past {_dateTime}";
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult(GetErrorMessage());
            DateTime date = (DateTime)value;

            if (DateTime.Compare(date, DateTime.Now) < 0) return new ValidationResult(GetErrorMessage());
            else return ValidationResult.Success;
        }
    }

}
