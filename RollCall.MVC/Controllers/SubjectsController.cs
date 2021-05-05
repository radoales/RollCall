namespace RollCall.MVC.Controllers
{
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using RollCall.MVC.ViewModels.Subjects;
    using Services;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using static RollCall.MVC.WebConstants;

    [Authorize]
    public class SubjectsController : Controller
    {
        private readonly ISubjectServices subjectServices;
        private readonly IAttendanceService attendanceService;
        private readonly UserManager<User> userManager;

        public SubjectsController(
            ISubjectServices subjectServices,
            IAttendanceService attendanceService,
            UserManager<User> userManager)
        {
            this.subjectServices = subjectServices;
            this.attendanceService = attendanceService;
            this.userManager = userManager;
        }

        // GET: Subjects
        public async Task<IActionResult> Index()
        {
            var userId =  this.userManager.GetUserId(this.User);
            var model = this.User.IsInRole(Roles.AdminRole) ? await this.subjectServices.GetAllSubjects()
                : await this.subjectServices.GetAllSubjectsByUser(userId);

            return View(model);
        }

        // GET: Subjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await this.subjectServices.GetIndexSubjectVM((int)id);

            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // GET: Subjects/Create
        [Authorize(Roles = Roles.AdminRole)]
        public IActionResult Create()
        {

            return View();
        }

        // POST: Subjects/Create
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Subject model)
        {
            if (ModelState.IsValid)
            {
                var id = await this.subjectServices.Create(model.Name);

                return RedirectToAction(nameof(Details), new { id });
            }
            return View(model);
        }

        // GET: Subjects/Edit/5
        [Authorize(Roles = Roles.AdminRole)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await this.subjectServices.Get((int)id);


            if (subject == null)
            {
                return NotFound();
            }

            var model = new AddOrRemoveUsersFromSubjectVM
            {
                UsersToAdd = await this.subjectServices.GetAddUsersToSubjectVM("", (int)id),
                UsersInSubject = await this.subjectServices.GetUsersInSubject((int)id),
                Subject = subject
            };

            return View(model);
        }

        // POST: Subjects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Subject model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await this.subjectServices.Update(id, model.Name);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Subjects/Delete/5
        [Authorize(Roles = Roles.AdminRole)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await this.subjectServices.Get((int)id);
            if (subject == null)
            {
                return NotFound();
            }

            var hasClassessOrUsers = await this.subjectServices.HasClassessOrUsers((int)id);

            if (hasClassessOrUsers)
            {
                TempData[TempDataErrorMessageKey] = "Can not delete subject if it has classes or users";
                return RedirectToAction(nameof(Details), new { id });
            }

            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.subjectServices.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetAdduserSubjectsVM(string name, int subjectId)
        {
            var model = await this.subjectServices.GetAddUsersToSubjectVM(name, subjectId);
            return PartialView("_AddUserToSubjectPartial", model);
        }

        [Authorize(Roles = Roles.AdminRole)]
        public async Task<IActionResult> AddUserToSubject(string userId, int subjectId)
        {
            await this.subjectServices.AddUserToSubject(userId, subjectId);

            return RedirectToAction(nameof(Edit), new { id = subjectId });
        }

        [Authorize(Roles = Roles.AdminRole)]
        public async Task<IActionResult> RemoveUserFromSubject(string userId, int subjectId)
        {
            //todo check if user has passed attendances in this subject
            var hasAttendances = await this.attendanceService.HasUserPassedAttendancesInSubject(userId, subjectId);

            if (hasAttendances)
            {
                TempData[TempDataErrorMessageKey] = "Can not remove user with pass classes";
                return RedirectToAction(nameof(Edit), new { id = subjectId });
            }
            await this.subjectServices.RemoveUserFromSubject(userId, subjectId);

            return RedirectToAction(nameof(Edit), new { id = subjectId });
        }
    }
}
