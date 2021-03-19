namespace RollCall.MVC.Data.Models
{
    using System;
    public class Attendance
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int ClassId { get; set; }
        public Class Class { get; set; }
    }
}
