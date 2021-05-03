namespace RollCall.MVC.ViewModels.Identity
{
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using static WebConstants;

   // [IgnoreAntiforgeryToken(Order = 1001)]
    public class LoginRequestModel
    {
        [Required(ErrorMessage = ErrorMessages.RequiredField)]
        public string Username { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredField)]
        public string Password { get; set; }
    }
}
