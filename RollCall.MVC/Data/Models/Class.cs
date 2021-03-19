namespace RollCall.MVC.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Class
    {
        public int Id { get; set; }

        [Display(Name ="Start time")]
        public DateTime ClassStartTime { get; set; }
        [Display(Name = "End time")]
        public DateTime ClassEndTime { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public ICollection<Attendance> Attendances { get; set; }
    }
}
