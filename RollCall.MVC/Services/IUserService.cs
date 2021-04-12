namespace RollCall.MVC.Services
{
    using RollCall.MVC.ViewModels.Users;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<IEnumerable<UserIndexVM>> GetAllUsersAsIndexVM();
        Task<IEnumerable<UserIndexVM>> GetAllTeachersStudentsAsIndexVM(string teacherId);
        Task<UserDetailVM> GetAsUserDetailVM(string id);
        Task<UserDetailVM> GetAsUserDetailVM(string id, int subjectId);

    }
}
