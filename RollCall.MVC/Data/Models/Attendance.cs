namespace RollCall.MVC.Data.Models
{
    using System;
    public class Attendance
    {
        public int Id { get; set; }

        public bool CheckIn_Start { get; set; }
        public bool CheckIn_Middle { get; set; }
        public bool CheckIn_End { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int ClassId { get; set; }
        public SchoolClass Class { get; set; }
    }
}
