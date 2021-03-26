namespace RollCall.MVC.Data.Models
{
    public class UserClasses
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public int SchoolClassId { get; set; }
        public SchoolClass SchoolClass { get; set; }
    }
}
