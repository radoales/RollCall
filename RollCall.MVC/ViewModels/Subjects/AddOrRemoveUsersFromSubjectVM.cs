namespace RollCall.MVC.ViewModels.Subjects
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using RollCall.MVC.Data.Models;
    using System.Collections.Generic;

    public class AddOrRemoveUsersFromSubjectVM
    {
        public int Id { get; set; } 

        public string Name { get; set; }

        public Subject Subject { get; set; }

        public IEnumerable<AddUsersToSubjectVM> UsersToAdd { get; set; }
        public IEnumerable<User> UsersInSubject { get; set; }

        public ICollection<SchoolClass> Classes { get; set; }
        public ICollection<UsersSubjects> UsersSubjects { get; set; }
    }
}
