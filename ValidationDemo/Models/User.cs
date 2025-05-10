using System.ComponentModel.DataAnnotations;
using ValidationDemo.Validators;

namespace ValidationDemo.Models
{
    public class User
    {
        [Required(ErrorMessage = "User ID is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Name must be between 5 to 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Invalid Phone Number.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "DateOfBirth is required")]
        [DataType(DataType.DateTime)]
        [AgeValidation(18, 99)]
        public DateTime DateOfBirth { get; set; }

        [Range(18, 99, ErrorMessage = "Age must be between 18 and 99.")]
        public int Age { get; set; }

        [RegularExpression(@"^\d{8}(-\d{4})?$", ErrorMessage = "Invalid Zip Code.")]
        public string ZipCode { get; set; }

        [Url(ErrorMessage = "Invalid Website URL.")]
        public string Website {  get; set; }

        [CreditCard(ErrorMessage = "Invalid Credit Card Number")]
        public string CreditCardNumber { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be least 6 characters")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
