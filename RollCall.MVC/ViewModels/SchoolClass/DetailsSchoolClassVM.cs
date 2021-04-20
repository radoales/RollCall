namespace RollCall.MVC.ViewModels.SchoolClass
{
    using RollCall.MVC.Data.Models;
    using RollCall.MVC.ViewModels.Attendance;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DetailsSchoolClassVM
    {
        public int Id { get; set; }

        public IEnumerable<User> Teachers { get; set; }
        public int CurrentBlock { get; set; }

        public double AverageAttendance
        {
            get
            {              
                return Attendances.Count > 0? Math.Ceiling(Attendances.Average(a => a.AttendancePersentage)): 0;
            }
            set
            {
                this.AverageAttendance = value;
            }
        }

        public string UserId { get; set; }

        public bool StudentCheckedIn { get; set; }

        public int? Code { get; set; }
        public int? EnteredCode { get; set; }

        public string Date { get; set; }

        public string CodeGeneratedTime { get; set; }

        public string Time { get; set; }

        public DateTime? TimeLeft { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public ICollection<ListAtendanceVM> Attendances { get; set; }
    }
}
