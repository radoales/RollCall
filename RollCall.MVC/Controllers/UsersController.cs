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
        private readonly ISubjectServices subjectServices;

        public UsersController(
            IUserService userService,
            UserManager<User> userManager, ISubjectServices subjectServices)
        {
            this.userService = userService;
            this.userManager = userManager;
            this.subjectServices = subjectServices;
        }

        public async Task<IActionResult> Index(int? pageNumber, string name)
        {
            var userid = this.userManager.GetUserId(this.User);

            var users = this.User.IsInRole(Roles.AdminRole) ? await this.userService.GetAllUsersAsIndexVM(name)
                : await this.userService.GetAllTeachersStudentsAsIndexVM(userid, name);


            var model = new PaginatedUserIndexVM
            {
                Users = await PaginatedList<UserIndexVM>.CreateAsync(users, pageNumber ?? 1, 10),
                Name = name
            };

            return View(model);
        }

        public async Task<IActionResult> Details(string id, int? subjectId)
        {
            var loggedInUser = this.userManager.GetUserId(this.User);

            var model = subjectId == null ? await this.userService.GetAsUserDetailVM(id, 0)
                : await this.userService.GetAsUserDetailVM(id, (int)subjectId);

            model.Subjects = this.subjectServices.GetUsersSubjectsAsSelectedList(loggedInUser);
            model.Subject = subjectId != null ? (int)subjectId : 0;
            return View(model);
        }

        public async Task<IActionResult> GetTeachersStudents(int? pageNumber, string name)
        {
            var userid = this.userManager.GetUserId(this.User);

            var users = this.User.IsInRole(Roles.AdminRole) ? await this.userService.GetAllUsersAsIndexVM(name)
                : await this.userService.GetAllTeachersStudentsAsIndexVM(userid, name);

            var model = await PaginatedList<UserIndexVM>.CreateAsync(users, pageNumber ?? 1, 2);

            return PartialView("_UsersPartial", model);
        }
    }
}
