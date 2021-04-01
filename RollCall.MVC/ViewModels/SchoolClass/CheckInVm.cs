namespace RollCall.MVC.ViewModels.SchoolClass
{
    using RollCall.MVC.Data.Models;
    using System;
    using System.Collections.Generic;
    public class CheckInVM
    {
        public int Id { get; set; }

        public string SubjectName { get; set; }

        public int ClassId { get; set; }

        public int CurrentBlock { get; set; }

        public string UserId { get; set; }

        public bool StudentCheckedIn { get; set; }
        public bool IsClassActive { get; set; }

        public int? Code { get; set; }
        public int? EnteredCode { get; set; }

        public string Date { get; set; }

        public string CodeGeneratedTime { get; set; }

        public string Time { get; set; }

        public DateTime? TimeLeft { get; set; }

    }
}
