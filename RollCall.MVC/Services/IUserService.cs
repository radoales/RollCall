namespace RollCall.MVC.Services
{
    using RollCall.MVC.ViewModels.Users;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<IEnumerable<UserIndexVM>> GetAllUsersAsIndexVM(string name);
        Task<IEnumerable<UserIndexVM>> GetAllTeachersStudentsAsIndexVM(string teacherId, string name);
        Task<UserDetailVM> GetAsUserDetailVM(string id);
        Task<UserDetailVM> GetAsUserDetailVM(string id, int subjectId);
    }
}
