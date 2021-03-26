namespace RollCall.MVC.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using RollCall.MVC.Data;
    using RollCall.MVC.Data.Models;
    using RollCall.MVC.Services;
    using RollCall.MVC.ViewModels.SchoolClass;
    using System.Linq;
    using System.Threading.Tasks;
    public class SchoolClassesController : Controller
    {
        private readonly ISchoolClassService schoolClassService;
        private readonly ISubjectServices subjectServices;

        public SchoolClassesController(ISchoolClassService schoolClassService, ISubjectServices subjectServices)
        {
            this.schoolClassService = schoolClassService;
            this.subjectServices = subjectServices;
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

            var schoolClass = await this.schoolClassService.Get((int)id);

            if (schoolClass == null)
            {
                return NotFound();
            }

            return View(schoolClass);
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
    }
}
