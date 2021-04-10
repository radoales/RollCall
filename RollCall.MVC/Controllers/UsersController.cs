namespace RollCall.MVC.Controllers
{
    using Data.Models;
    using Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using System.Threading.Tasks;
    using ViewModels.Users;
    using static RollCall.MVC.WebConstants;

    //[Authorize(Roles = Roles.AdminRole)]
    //[Authorize(Roles = Roles.TeacherRole)]
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;

        public UsersController(
            IUserService userService,
            UserManager<User> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(int? pageNumber)
        {
            var userid = this.userManager.GetUserId(this.User);

            var users = this.User.IsInRole(Roles.AdminRole) ? await this.userService.GetAllUsersAsIndexVM()
                : await this.userService.GetAllTeachersStudentsAsIndexVM(userid);

            var model = new PaginatedUserIndexVM
            {
                Users = await PaginatedList<UserIndexVM>.CreateAsync(users, pageNumber ?? 1, 5)
            };

            return View(model);
        }

        public async Task<IActionResult> Details (string id)
        {
            var model = await this.userService.GetAsUserDetailVM(id);
            return View(model);
        }
    }
}
