namespace RollCall.MVC.Services
{
    using RollCall.MVC.ViewModels.Users;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<IEnumerable<UserIndexVM>> GetAllUserIndexVMs();
    }
}
