using RollCall.MVC.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RollCall.MVC.ViewModels.SchoolClass
{
    public class EditSchoolClassVM
    {
        public int Id { get; set; }

        [Display(Name = "Start time")]
        public DateTime ClassStartTime { get; set; }

        [Display(Name = "End time")]
        public DateTime ClassEndTime { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
