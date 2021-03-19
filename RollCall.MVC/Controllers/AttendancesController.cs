namespace RollCall.MVC.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Data;
    using Data.Models;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using RollCall.MVC.Services;
    using RollCall.MVC.ViewModels.Attendance;

    // [Authorize]
    public class AttendancesController : Controller
    {
       // private readonly RollCallDbContext _context;
        private readonly IAttendanceService attendanceService;

        public AttendancesController(RollCallDbContext context, IAttendanceService attendanceService)
        {
          //  _context = context;
            this.attendanceService = attendanceService;
        }

        // GET: Attendances
        public async Task<IActionResult> Index()
        {
            var attendances = await this.attendanceService.GetAll();

            return View(attendances);
        }

        // GET: Attendances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await this.attendanceService
                .GetAttendance((int)id);

            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // GET: Attendances/Create
        public IActionResult Create()
        {
            //ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Id", attendance.ClassId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", attendance.UserId);
            return View();
        }

        // POST: Attendances/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateTime,UserId,ClassId")] CreateAttendanceVM model)
        {
            if (ModelState.IsValid)
            {
                await this.attendanceService.CreateAttendance(model.DateTime, model.UserId, model.ClassId);
                return RedirectToAction(nameof(Index));
            }
            //ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Id", attendance.ClassId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", attendance.UserId);
            return View(model);
        }

        // GET: Attendances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var attendance = await _context.Attendance.FindAsync(id);
            //if (attendance == null)
            //{
            //    return NotFound();
            //}
            //ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Id", attendance.ClassId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", attendance.UserId);
            return View();
        }

        // POST: Attendances/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateTime,UserId,ClassId")] Attendance attendance)
        {
            //if (id != attendance.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(attendance);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!AttendanceExists(attendance.Id))
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
            //ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Id", attendance.ClassId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", attendance.UserId);
            return View(attendance);
        }

        // GET: Attendances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await this.attendanceService
                .GetAttendance((int)id);

            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attendance = await this.attendanceService
                .GetAttendance((int)id);

            await this.attendanceService.DeleteAttendance(attendance);
            return RedirectToAction(nameof(Index));
        }

        //private bool AttendanceExists(int id)
        //{
        //    return _context.Attendance.Any(e => e.Id == id);
        //}
    }
}
