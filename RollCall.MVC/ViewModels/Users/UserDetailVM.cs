namespace RollCall.MVC.ViewModels.Users
{
    using Data.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using RollCall.MVC.ViewModels.Attendance;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class UserDetailVM
    {
        public string Id { get; set; }
        [DisplayName("Name")]
        public string Name { get; set; }

        public string Email { get; set; }

        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        [DisplayName("Student №")]
        public int? StudentNumber { get; set; }

        public ICollection<ListAtendanceVM> Attendances { get; set; }

        public ICollection<UsersSubjects> UsersSubjects { get; set; }

        public SelectList Subjects { get; set; }

        public int Subject { get; set; }
    }
}
