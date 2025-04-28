using System.ComponentModel.DataAnnotations;

namespace Client
{
    public class DateFromNowAttribute : ValidationAttribute
    {
        private readonly DateTime _dateTime = DateTime.Now;
        public DateFromNowAttribute() { }
        public string GetErrorMessage() => $"Date must be past {_dateTime}";
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var date = (DateTime)value;

            if (DateTime.Compare(date, DateTime.Now) < 0) return new ValidationResult(GetErrorMessage());
            else return ValidationResult.Success;
        }
    }

}
