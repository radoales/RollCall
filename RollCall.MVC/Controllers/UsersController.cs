using Microsoft.AspNetCore.Mvc;
using RollCall.MVC.Helpers;
using RollCall.MVC.Services;
using RollCall.MVC.ViewModels.Users;
using System.Threading.Tasks;

namespace RollCall.MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<IActionResult> Index(int? pageNumber)
        {
            var users = await this.userService.GetAllUserIndexVMs();

            var model = new PaginatedUserIndexVM
            {
                Users = await PaginatedList<UserIndexVM>.CreateAsync(users, pageNumber ?? 1, 2)
            };

            return View(model);
        }
    }
}
