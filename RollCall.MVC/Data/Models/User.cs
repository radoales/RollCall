
namespace RollCall.MVC.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel;
    public class User : IdentityUser
    {

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public override string Email { get; set; }

        [DisplayName("Phone Number")]
        public override string PhoneNumber { get; set; }

        [DisplayName("Student №")]
        public int? StudentNumber { get; set; }

        public ICollection<Attendance> Attendances { get; set; }

        public ICollection<UsersSubjects> UsersSubjects { get; set; }
    }
}
