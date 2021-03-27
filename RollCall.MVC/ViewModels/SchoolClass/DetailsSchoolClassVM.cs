namespace RollCall.MVC.ViewModels.SchoolClass
{
    using RollCall.MVC.Data.Models;
    using System;
    using System.Collections.Generic;
    public class DetailsSchoolClassVM
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int? Code { get; set; }

        public string Date { get; set; }

        public string CodeGeneratedTime { get; set; }

        public string Time { get; set; }

        public DateTime? TimeLeft { get; set; }

        public double AttendancePercentage { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public ICollection<Attendance> Attendances { get; set; }

        public ICollection<UserClasses> UserClasses { get; set; }
    }
}
