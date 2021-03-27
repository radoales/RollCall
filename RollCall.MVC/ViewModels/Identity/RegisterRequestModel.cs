namespace RollCall.MVC.ViewModels.Identity
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using RollCall.MVC.Data.Models;
    using static WebConstants;
    public class RegisterRequestModel
    {
        [Required(ErrorMessage = ErrorMessages.RequiredField)]
        [MinLength(5, ErrorMessage = "Username is too short")]
        [MaxLength(20, ErrorMessage = "Username is too long")]
        [RegularExpression(@"^[a-zA-Z0-9_.-]*$", ErrorMessage = "Invalid Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredField)]
        [MaxLength(200, ErrorMessage = "Email too long")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredField)]
        [MinLength(8, ErrorMessage = "Password too short")]
        [MaxLength(16, ErrorMessage = "Password too long")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        public SelectList Roles { get; set; }

        public string Role { get; set; }
    }
}
