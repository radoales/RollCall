namespace RollCall.MVC.ViewModels.SchoolClass
{
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class IndexSchoolClassVM
    {
        public int Id { get; set; }

        public int? Code { get; set; }

        [Display(Name = "Start time")]
        public DateTime ClassStartTime { get; set; }

        [Display(Name = "End time")]
        public DateTime ClassEndTime { get; set; }

        public int UsersInClass { get; set; }

        public int Participants { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public ICollection<Attendance> Attendances { get; set; }
    }
}
