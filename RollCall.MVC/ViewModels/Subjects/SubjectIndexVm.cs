namespace RollCall.MVC.ViewModels.Subjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using static WebConstants;

    public class SubjectIndexVm
    {
        public IEnumerable<Subject> Subjects { get; set; }

        public SelectList SubjectSlots { get; set; } = new SelectList(new List<string>() { OngoingSubjects, PastSubjects, UpcomingSubjects}, OngoingSubjects);

        public string SubjectSlot { get; set; }
    }
}
