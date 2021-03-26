namespace RollCall.MVC.ViewModels.SchoolClass
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using RollCall.MVC.Data.Models;
    using System;
    using System.ComponentModel.DataAnnotations;
    public class CreateSchoolClassVM
    {
        [Display(Name = "Start time")]
        public DateTime ClassStartTime { get; set; }

        [Display(Name = "End time")]
        public DateTime ClassEndTime { get; set; }

        public int SubjectId { get; set; }

        public SelectList Subjects { get; set; }
    }
}
