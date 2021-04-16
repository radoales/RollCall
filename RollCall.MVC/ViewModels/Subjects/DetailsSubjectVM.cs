namespace RollCall.MVC.ViewModels.Subjects
{
    using RollCall.MVC.Data.Models;
    using System.Collections.Generic;
    public class DetailsSubjectVM
    {
        public Subject Subject { get; set; }
        public IEnumerable<SchoolClass> SchoolClasses { get; set; }
        public IEnumerable<User> Students { get; set; }
        public IEnumerable<User> Teachers { get; set; }

    }
}
