namespace RollCall.MVC.ViewModels.Attendance
{
    using RollCall.MVC.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ListAtendanceVM
    {
        private readonly double attendancePercentage;
        private readonly string date;
        public int Id { get; set; }

        public bool CheckIn_Start { get; set; }
        public bool CheckIn_Middle { get; set; }
        public bool CheckIn_End { get; set; }

        public double AttendancePersentage
        {
            get
            {
                var list = new List<bool>() { CheckIn_Start, CheckIn_Middle, CheckIn_End };
                double count = Math.Floor(list.Count(x => x == true) / 3.00 * 100);
                return count;
            }
            set
            {
                this.AttendancePersentage = value;
            }
        }

        public string UserId { get; set; }
        public User User { get; set; }

        public int ClassId { get; set; }
        public SchoolClass Class { get; set; }

        public string Date
        {
            get
            {             
                return Class.ClassStartTime.GetDateTimeFormats('D')[0];
            }
            set
            {
                this.Date = value;
            }
        }
    }
}
