using System.ComponentModel.DataAnnotations;

namespace ValidationDemo.Validators
{
    public class AgeValidationAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;
        private readonly int _maximumAge;

        public AgeValidationAttribute(int minimumAge = 18, int maximumAge = 99)
        {
            _maximumAge = maximumAge;
            _minimumAge = minimumAge;

            ErrorMessage = $"Age must be between {_minimumAge} and {_maximumAge} years";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            
            if (value == null)
            {
                return new ValidationResult("Date of Birth is required.");
            }

            if(!(value is DateTime dateOfBirth))
            {
                return new ValidationResult("Invalid date of birth format.");
            }

            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;

            // Adjust age if the birthday hasn't occurred yet this year
            if (dateOfBirth.Date > today.AddYears(-age))
                age--;
            if (age < _minimumAge || age > _maximumAge)
            {
                return new ValidationResult(ErrorMessage);
            }
            // Returning null indicates that the validation was successful
            return ValidationResult.Success;
        }
    }
}
