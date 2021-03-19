namespace RollCall.MVC.Data.Models
{
    public class UsersSubjects
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
