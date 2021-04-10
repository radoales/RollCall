namespace RollCall.MVC.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Data.Models;
    using System.Linq;
    using System.Threading.Tasks;
    public class UserIndexVM
    {
        [DisplayName("Name")]
        public string Name { get; set; }

        public string Email { get; set; }

        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        [DisplayName("Student №")]
        public int? StudentNumber { get; set; }

        public ICollection<Attendance> Attendances { get; set; }

        public ICollection<UsersSubjects> UsersSubjects { get; set; }
    }
}
