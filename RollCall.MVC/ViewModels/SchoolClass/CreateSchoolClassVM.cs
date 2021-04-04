namespace RollCall.MVC.ViewModels.SchoolClass
{
    using ExpressiveAnnotations.Attributes;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.ComponentModel.DataAnnotations;
    public class CreateSchoolClassVM
    {
        [Display(Name = "Start time")]
        public DateTime ClassStartTime { get; set; }

        [Display(Name = "End time")]
        [AssertThat("ClassEndTime >= ClassStartTime", ErrorMessage ="End time can not be before Start time")]
        public DateTime ClassEndTime { get; set; }

        [Display(Name = "Subject")]
        public int SubjectId { get; set; }

        public SelectList Subjects { get; set; }


    }
}
