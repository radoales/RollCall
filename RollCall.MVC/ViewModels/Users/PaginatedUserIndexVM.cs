namespace RollCall.MVC.ViewModels.Users
{
    using RollCall.MVC.Helpers;

    public class PaginatedUserIndexVM
    {
        public PaginatedList<UserIndexVM> Users { get; set; }
        public string Name { get; set; }
    }
}
