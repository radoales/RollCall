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

    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;
        private readonly ISubjectServices subjectServices;

        public UsersController(
            IUserService userService,
            UserManager<User> userManager,
            ISubjectServices subjectServices)
        {
            this.userService = userService;
            this.userManager = userManager;
            this.subjectServices = subjectServices;
        }

        [Authorize(Roles = Roles.AdminRole + "," + Roles.TeacherRole)]
        public async Task<IActionResult> Index(int? pageNumber, string name)
        {
            var userid = this.userManager.GetUserId(this.User);

            if (this.User.IsInRole(Roles.StudentRole))
            {
                RedirectToAction(nameof(Details), new { id = userid });
            }

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
            var loggedInUserId = this.userManager.GetUserId(this.User);
            if (string.IsNullOrEmpty(id))
            {
                id = loggedInUserId;
            }

            var isUserTeacherOrAdmin = this.User.IsInRole(Roles.AdminRole) || this.User.IsInRole(Roles.TeacherRole);

            if (id != loggedInUserId && !isUserTeacherOrAdmin)
            {
                return StatusCode(403);
            }

            var model = subjectId == null ? await this.userService.GetAsUserDetailVM(id, 0)
                : await this.userService.GetAsUserDetailVM(id, (int)subjectId);

            model.Subjects = this.subjectServices.GetUsersSubjectsAsSelectedList(loggedInUserId, id);
            model.Subject = subjectId != null ? (int)subjectId : 0;
            return View(model);
        }

        public async Task<IActionResult> GetTeachersStudents(int? pageNumber, string name)
        {
            var userid = this.userManager.GetUserId(this.User);

            var users = this.User.IsInRole(Roles.AdminRole) ? await this.userService.GetAllUsersAsIndexVM(name)
                : await this.userService.GetAllTeachersStudentsAsIndexVM(userid, name);

            var model = await PaginatedList<UserIndexVM>.CreateAsync(users, pageNumber ?? 1, 10);

            return PartialView("_UsersPartial", model);
        }
    }
}
