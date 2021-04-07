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
        private readonly RollCallDbContext _context;
        private readonly ISubjectServices subjectServices;
        private readonly IAttendanceService attendanceService;
        private readonly UserManager<User> userManager;

        public SubjectsController(
            RollCallDbContext context,
            ISubjectServices subjectServices,
            IAttendanceService attendanceService,
            UserManager<User> userManager)
        {
            _context = context;
            this.subjectServices = subjectServices;
            this.attendanceService = attendanceService;
            this.userManager = userManager;
        }

        // GET: Subjects
        public async Task<IActionResult> Index()
        {
            var userId =  this.userManager.GetUserId(this.User);
            var model = await this.subjectServices.GetAllSubjectsByUser(userId);

            return View(model);
        }

        // GET: Subjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context
                .Subjects
                .Include(x => x.Classes)
                .ThenInclude(x => x.Attendances)
                .FirstOrDefaultAsync(m => m.Id == id);

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Subject subject)
        {
            if (id != subject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectExists(subject.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        // GET: Subjects/Delete/5
        [Authorize(Roles = Roles.AdminRole)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectExists(int id)
        {
            return _context.Subjects.Any(e => e.Id == id);
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
