using System.Collections.Generic;

namespace RollCall.MVC.Data.Models
{
    public class Subject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Class> Classes { get; set; } 
        public ICollection<UsersSubjects> UsersSubjects { get; set; }
    }
}
