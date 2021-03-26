namespace RollCall.MVC.ViewModels.Attendance
{
    using Data.Models;
    using System;

    public class CreateAttendanceVM
    {
        public DateTime DateTime { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int ClassId { get; set; }
        public SchoolClass Class { get; set; }
    }
}
