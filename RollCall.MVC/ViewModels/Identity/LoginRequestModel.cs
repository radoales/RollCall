namespace RollCall.MVC.ViewModels.Identity
{
    using System.ComponentModel.DataAnnotations;
    using static WebConstants;

    public class LoginRequestModel
    {
        [Required(ErrorMessage = ErrorMessages.RequiredField)]
        public string Username { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredField)]
        //[MinLength(8, ErrorMessage = "Incorrect username or password")]
        //[MaxLength(16, ErrorMessage = "Incorrect username or password")]
        public string Password { get; set; }
    }
}
