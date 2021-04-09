namespace RollCall.MVC.ViewModels.SchoolClass
{
    using RollCall.MVC.Helpers;

    public class PaginatedListIndexSchoolClassVM
    {
        public PaginatedList<IndexSchoolClassVM> SchoolClasses { get; set; }
        public string Set { get; set; }
    }
}
