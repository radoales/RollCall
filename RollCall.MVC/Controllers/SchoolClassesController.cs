namespace RollCall.MVC.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Data.Models;
    using Helpers;
    using Services;
    using ViewModels.SchoolClass;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using static WebConstants;
    [Authorize]
    public class SchoolClassesController : Controller
    {
        private readonly ISchoolClassService schoolClassService;
        private readonly ISubjectServices subjectServices;
        private readonly IAttendanceService attendanceService;
        private readonly UserManager<User> userManager; 

        public SchoolClassesController(
            ISchoolClassService schoolClassService,
            ISubjectServices subjectServices,
            IAttendanceService attendanceService,
            UserManager<User> userManager)
        {
            this.schoolClassService = schoolClassService;
            this.subjectServices = subjectServices;
            this.attendanceService = attendanceService;
            this.userManager = userManager;
        }

        // GET: SchoolClasses 
        public async Task<IActionResult> Index(int? pageNumber, string schoolClassesSet)
        {
           var userId = this.userManager.GetUserId(this.User);
            var isAdmin = this.User.IsInRole(Roles.AdminRole);

            var schoolClasses = isAdmin switch
            {
                true => await this.schoolClassService.GetAllAsIndexSchoolClassesVm(schoolClassesSet),
                false => await this.schoolClassService.GetAllAsIndexSchoolClassesVmByUser(userId, schoolClassesSet)
            };

            var model = new PaginatedListIndexSchoolClassVM
            {
                SchoolClassesSet = schoolClassesSet,
                SchoolClasses = await PaginatedList<IndexSchoolClassVM>.CreateAsync(schoolClasses, pageNumber ?? 1, 5)
            };

            return View(model); 
        }

        // GET: SchoolClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await this.userManager.GetUserAsync(this.User);

            var isUserStudent = this.User.IsInRole(Roles.StudentRole);
            var userId = this.userManager.GetUserId(this.User);

            DetailsSchoolClassVM model = isUserStudent
                ? await this.schoolClassService.GetAsStudent((int)id, userId)
                : await this.schoolClassService.GetDetailsSchoolClassVM((int)id);

            if (model == null)
            {
                return NotFound();
            }

            var currentBlock = await this.schoolClassService.GetCurrentBlock((int)id);

            if (!isUserStudent)
            {
                return View(model);
            }

            model.UserId = userId;
            model.StudentCheckedIn = await this.attendanceService.IsStudentCheckedInforCurrentBlock(userId, (int)id, currentBlock);
            var isCheckInActive = await this.schoolClassService.GetCurrentBlock((int)id) != 0 && await this.schoolClassService.IsCheckInActive((int) id);

            if (!model.StudentCheckedIn && isCheckInActive)
            {
                return RedirectToAction(nameof(CheckIn), new { model.UserId, classId = model.Id });
            }

            return View(model);
        }

        // GET: SchoolClasses/Create
        [Authorize(Roles = Roles.AdminRole)]
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["SubjectId"] = this.subjectServices.GetSubjectsAsSelectedList();
            return View();
        }

        // POST: SchoolClasses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.AdminRole)]
        public async Task<IActionResult> Create([Bind("Id,Code,ClassStartTime,ClassEndTime,SubjectId")] CreateSchoolClassVM model)
        {
            if (ModelState.IsValid)
            {
                await this.schoolClassService.Create(model.ClassStartTime, model.ClassEndTime, model.SubjectId);
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubjectId"] = this.subjectServices.GetSubjectsAsSelectedList();
            return View(model);
        }

        // GET: SchoolClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await this.schoolClassService.GetEditSchoolClassVM((int)id);
            if (model == null)
            {
                return NotFound();
            }
            ViewData["SubjectId"] = this.subjectServices.GetSubjectsAsSelectedList();
            return View(model);
        }

        // POST: SchoolClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClassStartTime,ClassEndTime,SubjectId")] EditSchoolClassVM model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await this.schoolClassService.Update(model.Id, model.ClassStartTime, model.ClassEndTime, model.SubjectId);
                }
                catch (DbUpdateConcurrencyException)
                {

                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubjectId"] = this.subjectServices.GetSubjectsAsSelectedList();
            return View(model);
        }

        // GET: SchoolClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolClass = await this.schoolClassService.GetEditSchoolClassVM((int)id);

            if (schoolClass == null)
            {
                return NotFound();
            }

            return View(schoolClass);
        }

        // POST: SchoolClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.schoolClassService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GenerateCode(int id)
        {
            await this.schoolClassService.GenerateCode(id);

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> CheckIn(string userId, int classId)
        {
            var schoolClass = await this.schoolClassService.GetDetailsSchoolClassVM(classId);
            var isClassActive = await this.schoolClassService.GetCurrentBlock(classId) != 0;

            var model = new CheckInVM
            {
                SubjectName = schoolClass.Subject.Name,
                EnteredCode = schoolClass.EnteredCode,
                CodeGeneratedTime = schoolClass.CodeGeneratedTime,
                Date = schoolClass.Date,
                Time = schoolClass.Time,
                TimeLeft = schoolClass.TimeLeft,
                ClassId = classId,
                IsClassActive = isClassActive,
                CurrentBlock = await this.schoolClassService.GetCurrentBlock(classId)

            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CheckIn(CheckInVM model)
        {
            var isCheckInActive = await this.schoolClassService.IsCheckInActive(model.ClassId);

            if (!isCheckInActive)
            {
                TempData[TempDataErrorMessageKey] = "You did not check in succesfully!\n Check in was locked!";
                 return RedirectToAction(nameof(Details), new { id = model.ClassId });
            }

            var schoolClass = await this.schoolClassService.GetDetailsSchoolClassVM(model.ClassId);
            if (schoolClass.Code != model.EnteredCode)
            {
                var isClassActive = await this.schoolClassService.GetCurrentBlock(model.ClassId) != 0;
                model.IsClassActive = isClassActive;
                ModelState.AddModelError("EnteredCode", "Wrong Code");
                return View(model);
            }

            int currentBlock = await this.schoolClassService.GetCurrentBlock(model.ClassId);

            await this.attendanceService.CheckIn(model.UserId, model.ClassId, currentBlock);
            TempData[TempDataSuccessMessageKey] = $"Check In for block {model.CurrentBlock} was Succesfull!";
            return RedirectToAction(nameof(Details), new { id = model.ClassId });
        }
    }
}
