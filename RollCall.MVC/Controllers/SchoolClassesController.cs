﻿namespace RollCall.MVC.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using RollCall.MVC.Data;
    using RollCall.MVC.Data.Models;
    using RollCall.MVC.Services;
    using RollCall.MVC.ViewModels.SchoolClass;
    using System.Linq;
    using System.Threading.Tasks;
    using static RollCall.MVC.WebConstants;

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
        public async Task<IActionResult> Index()
        {
            var classes = await this.schoolClassService.GetAll();

            return View(classes);
        }

        // GET: SchoolClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isUserStudent = this.User.IsInRole(Roles.StudentRole);
            var userId = this.userManager.GetUserId(this.User);

            DetailsSchoolClassVM model = null;

            if (isUserStudent)
            {
                model = await this.schoolClassService.GetAsStudent((int)id, userId);
            }
            else
            {
                model = await this.schoolClassService.Get((int)id);
            }

            
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
            var isClassActive = await this.schoolClassService.GetCurrentBlock((int)id) != 0;

            if (!model.StudentCheckedIn && isClassActive)
            {
                return RedirectToAction(nameof(CheckIn), new { model.UserId, classId = model.Id });
            }

            return View(model);
        }

        // GET: SchoolClasses/Create
        public IActionResult Create()
        {
            ViewData["SubjectId"] =  this.subjectServices.GetSubjectsAsSelectedList();
            return View();
        }

        // POST: SchoolClasses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var schoolClass = await _context.SchoolClasses.FindAsync(id);
            //if (schoolClass == null)
            //{
            //    return NotFound();
            //}
            //ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id", schoolClass.SubjectId);
            return View(/*schoolClass*/);
        }

        // POST: SchoolClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,ClassStartTime,ClassEndTime,SubjectId")] SchoolClass schoolClass)
        {
            //if (id != schoolClass.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(schoolClass);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!SchoolClassExists(schoolClass.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id", schoolClass.SubjectId);
            return View(schoolClass);
        }

        // GET: SchoolClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var schoolClass = await _context.SchoolClasses
            //    .Include(s => s.Subject)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (schoolClass == null)
            //{
            //    return NotFound();
            //}

            return View(/*schoolClass*/);
        }

        // POST: SchoolClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var schoolClass = await _context.SchoolClasses.FindAsync(id);
            //_context.SchoolClasses.Remove(schoolClass);
            //await _context.SaveChangesAsync();
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
            var schoolClass = await this.schoolClassService.Get(classId);
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
                IsClassActive =  isClassActive

            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CheckIn(CheckInVM model)
        {
            var schoolClass = await this.schoolClassService.Get(model.ClassId);
            if (schoolClass.Code != model.EnteredCode)
            {
                ModelState.AddModelError("EnteredCode", "Wrong Code");
                return View(model);
            }

            int currentBlock = await this.schoolClassService.GetCurrentBlock(model.ClassId);

            await this.attendanceService.CheckIn(model.UserId, model.ClassId, currentBlock);

            return RedirectToAction(nameof(Details),  new { id = model.ClassId } );
        }
    }
}
