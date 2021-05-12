namespace RollCall.MVC.ViewModels.SchoolClass
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using RollCall.MVC.Helpers;
    using System.Collections.Generic;
    using static WebConstants;

    public class PaginatedListIndexSchoolClassVM
    {
        public PaginatedList<IndexSchoolClassVM> SchoolClasses { get; set; }
        public string SchoolClassesSet { get; set; }

        public SelectList SchoolClassesSets { get; set; } = 
            new SelectList(new List<string>() { OngoingClasses, PastClasses, UpcomingClasses }, OngoingClasses);
    }
}
