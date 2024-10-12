using System.ComponentModel.DataAnnotations;

namespace Company.Web.Models
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{8,}$",ErrorMessage="Password must be at least 8 characters");
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirming Password is Required")]
        [Compare(nameof(Password),ErrorMessage ="Passwords don't match")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Is Active is Required")]
        public bool IsActive { get; set; }

    }
}
